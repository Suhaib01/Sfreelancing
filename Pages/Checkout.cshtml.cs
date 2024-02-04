using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text;
using System.Text.Json.Nodes;
using VUA.Core.IRepositories;
using VUA.Core.IRepositories.IService;
using VUA.Core.Models;
using VUA.EF.Repositories;

namespace VUA.UI.Pages
{
    [IgnoreAntiforgeryToken]
    public class CheckoutModel : PageModel
    {
        public string ClientId { get; set; }
        private string ClientSecret { get; set; }
        public string PaypalUrl { get; set; }
        public string coursPrice { get; set; } = "";
        public string courseName { get; set; } = "";
        public int ID { get; set; } 
        private IBaseRepository<UserPayment> _userPaymentRepository;
        private IBaseRepository<Payment> _paymentRepository;
        private IAccountRepositorory _accountRepositorory;
        private IUserService _userService;
        public CheckoutModel(IConfiguration configuration,
            IBaseRepository<UserPayment> userPaymentRepository,
            IAccountRepositorory accountRepositorory,
            IUserService userService,
            IBaseRepository<Core.Models.Payment> paymentRepository)
        {
            ClientId = configuration["PayPalSettings:ClientId"]!;
            PaypalUrl = configuration["PayPalSettings:Url"]!;
            ClientSecret = configuration["PayPalSettings:Secret"]!;
            _userPaymentRepository = userPaymentRepository;
            _accountRepositorory = accountRepositorory;
            _userService = userService;
            _paymentRepository = paymentRepository;

        }
        public void OnGet()
        {
            courseName = TempData["Name"]?.ToString() ?? "";
            coursPrice = TempData["Total"]?.ToString() ?? "";
            ID = int.Parse(TempData["ID"]?.ToString() ?? "");
            TempData.Keep();
            if (courseName == "" || coursPrice == "")
            {
                Response.Redirect("/");
                return;
            }
        }

        public  JsonResult OnPostCreateOrder()
        {
            courseName = TempData["Name"]?.ToString() ?? "";
            coursPrice = TempData["Total"]?.ToString() ?? "";
            ID = int.Parse(TempData["ID"]?.ToString() ?? "");
            TempData.Keep();
            if (courseName == "" || coursPrice == "")
            {
                return new JsonResult("");

            }

            JsonObject creatOrderRequest = new JsonObject();
            creatOrderRequest.Add("intent", "CAPTURE");


            JsonObject amount = new JsonObject();
            amount.Add("currency_code", "USD");
            amount.Add("value", coursPrice);

            JsonObject purchaseUnit1 = new JsonObject();
            purchaseUnit1.Add("amount", amount);

            JsonArray purchaseUnit = new JsonArray();
            purchaseUnit.Add(purchaseUnit1);

            creatOrderRequest.Add("purchase_units", purchaseUnit);

            string accessToken = GetPaypaleAccessToken();

            string url = PaypalUrl + "/v2/checkout/orders";

            string orderId = "";
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);
                var requestMessage = new HttpRequestMessage(HttpMethod.Post, url);
                requestMessage.Content = new StringContent(creatOrderRequest.ToJsonString(), null, "application/json");
                var responseTask = client.SendAsync(requestMessage);
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsStringAsync();
                    readTask.Wait();

                    var strResponse = readTask.Result;
                    var jsonResponse = JsonNode.Parse(strResponse);
                    if (jsonResponse != null)
                    {
                        orderId = jsonResponse["id"]?.ToString() ?? "";
                        
                    }
                }

            }

            var response = new
            {
                Id = orderId
            };


            return new JsonResult(response);
        }
        public  async Task<JsonResult> OnPostCompleteOrder([FromBody] JsonObject data)
        {
            ID = int.Parse(TempData["ID"]?.ToString() ?? "");
            courseName = TempData["Name"]?.ToString() ?? "";
            coursPrice = TempData["Total"]?.ToString() ?? "";
            if (data == null || data["orderID"] == null) return new JsonResult("");
            var orderID = data["orderID"]!.ToString();
            string accessToken = GetPaypaleAccessToken();

            string url = PaypalUrl + "/v2/checkout/orders/" + orderID + "/capture";
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);

                var requestMessage = new HttpRequestMessage(HttpMethod.Post, url);
                requestMessage.Content = new StringContent("", null, "application/json");


                var responseTask = client.SendAsync(requestMessage);
                responseTask.Wait();


                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsStringAsync();
                    readTask.Wait();

                    var strResponse = readTask.Result;
                    var jsonResponse = JsonNode.Parse(strResponse);
                    if (jsonResponse != null)
                    {
                        string paypalOrderStatus = jsonResponse["status"]?.ToString() ?? "";
                        if (paypalOrderStatus == "COMPLETED")
                        {
                            _accountRepositorory.AddCourse(ID);
                            var userid = _userService.GetUserId();
                     
                            var payment = new Payment
                            {
                                OrderId = orderID,
                                ProductName = courseName,
                                ProductPrice = coursPrice,
                                PaymentDate = DateTime.Now,
                                PaymentStatus = "Success"
                            };
                            _paymentRepository.Add(payment);
                            var userPayment = new UserPayment
                            {
                                PaymentId = payment.PaymentId,
                                UserId = userid,
                            };
                            _userPaymentRepository.Add(userPayment);

                            TempData.Clear();
                            return new JsonResult("success");
                        }

                    }
                }

            }

            return new JsonResult("");
        }
        public JsonResult OnPostCancelOrder([FromBody] JsonObject data)
        {
            ID = int.Parse(TempData["ID"]?.ToString() ?? "");
            courseName = TempData["Name"]?.ToString() ?? "";
            coursPrice = TempData["Total"]?.ToString() ?? "";
            if (data == null || data["orderID"] == null) return new JsonResult("");
            var orderID = data["orderID"]!.ToString();
            var userid = _userService.GetUserId();
            
            var payment = new Payment
            {
                OrderId = orderID,
                ProductName = courseName,
                ProductPrice = coursPrice,
                PaymentDate = DateTime.Now,
                PaymentStatus = "Failed"
            };
            _paymentRepository.Add(payment);
            var userPayment = new UserPayment
            {
                PaymentId = payment.PaymentId,
                UserId = userid,
            };
            _userPaymentRepository.Add(userPayment);
            return new JsonResult("");
        }



        private string GetPaypaleAccessToken()
        {
            string token = "";
            string url = PaypalUrl + "/v1/oauth2/token";
            using (var client = new HttpClient())
            {
                string credentials64 =
                    Convert.ToBase64String(Encoding.UTF8.GetBytes(ClientId + ":" + ClientSecret));
                client.DefaultRequestHeaders.Add("Authorization", "Basic " + credentials64);

                var requestMessage = new HttpRequestMessage(HttpMethod.Post, url);
                requestMessage.Content = new StringContent("grant_type=client_credentials", null, "application/x-www-form-urlencoded");
                var responseTask = client.SendAsync(requestMessage);
                responseTask.Wait();
                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsStringAsync();
                    readTask.Wait();

                    var strResponse = readTask.Result;
                    var jsonResponse = JsonNode.Parse(strResponse);
                    if (jsonResponse != null)
                    {
                        token = jsonResponse["access_token"]?.ToString() ?? "";
                    }
                }
            }
            return token;
        }
    }
}
