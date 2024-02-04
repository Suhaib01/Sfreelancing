using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VUA.Core.Models
{
    public class Payment
    {

        public int PaymentId { get; set; }
        public string? ProductName { get; set; }
        public string? ProductPrice { get; set; }
        public string? OrderId { get; set;}
        public DateTime PaymentDate { get; set; }
        public string? PaymentStatus { get; set; }
        public ICollection<UserPayment>? UserPayments { get; set; }
       

    }
}
