using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using VUA.Core.IRepositories;
using VUA.Core.Models;
using VUA.UI.Models;

namespace VUA.UI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IBaseRepository<Contact> _contactRepository;
        private readonly UserManager<AppllicationUser> _userManager;
        public HomeController(ILogger<HomeController> logger, IBaseRepository<Contact> contactRepository,
            UserManager<AppllicationUser> userManager)
        {
            _logger = logger;
            _contactRepository = contactRepository;
            _userManager = userManager;
        }
        

        [HttpGet]
        [Authorize]
        public IActionResult Redirection()
        {
            var user = _userManager.GetUserAsync(User).Result;
            switch (user!.StudantOrTeacher)
            {
                case "Studant":
                    return RedirectToAction("Index", "Dashboard", new { area = "AppUsers" });
                case "Teacher":
                    if(_userManager.IsInRoleAsync(user!, "Teacher").Result && _userManager.IsInRoleAsync(user!, "AppUsers").Result)
                    {
                        return RedirectToAction("TeacherCourses", "Dashboard", new { area = "AppUsers" });
                    }
                    else
                    {
                        return RedirectToAction("Index", "Dashboard", new { area = "AppUsers" });
                    }
                    
                default:
                    var isAdmin = _userManager.IsInRoleAsync(user!, "Admin").Result;
                    if (isAdmin)
                    {
                        return RedirectToAction("Courses", "Dashboard", new { area = "Admin" });
                    }
                    else
                    {
                        return RedirectToAction("Index", "Dashboard", new { area = "HiÇ" });
                    }

            }

        }

        public IActionResult Index()
        {
            ViewBag.Co = false;
            return View();
        }
        public IActionResult AboutUs()
        {

            return View();
        }
        public IActionResult OurTeachers()
        {
            ViewBag.Our = "Teachers";
            return View("AboutUs");
        }
        public IActionResult OurSchool()
        {
            ViewBag.Our = "School";
            return  View("AboutUs");
        }

        public IActionResult Admissions()
        {
            return View();
        }
        public IActionResult Courses()
        {
            ViewBag.Co = true;
            return View();
        }
        [HttpGet]
        public IActionResult Contact()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Contact(Contact SenderMessage) 
        { 
            if(ModelState.IsValid)
            {
                try
                {
                    _contactRepository.Add(SenderMessage);
                    ViewData["SubmittedData"] =SenderMessage;
                    return View("Done");

                }
                catch (Exception)
                {

                    throw;
                }
            
            }
            return View(SenderMessage);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}