using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VUA.Core.Models
{
    public class Contact
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Enter First Name please")]
        public string? FirsName { get; set; }
        [Required(ErrorMessage = "Enter First Name please")]   
        public string? LastName { get; set;}
        [Required]
        [RegularExpression(@"^[0-9]{14}$", ErrorMessage = "Please enter a valid 14-digit mobile number.")]
        public string? PhoneNumber { get; set;}
        [Required(ErrorMessage = "Email address is required.")]
        [EmailAddress(ErrorMessage = "email format should be : Example@email.ceo")]
        public string? Email { get; set;}
        [Required(ErrorMessage = "The field cannot be empty")]    
        public string? Message { get; set;}
        public bool isReplyed { get; set; }
        public bool isDeleted { get; set; }
    }
}
