using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VUA.Core.Models
{
    public class UserPayment
    {
        public string? UserId { get; set; }
        public AppllicationUser? User { get; set; }
        public int PaymentId { get; set; }
        public Payment? Payment { get; set; }
       
    }
}
