using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VUA.Core.Models.ViewModels
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Enter your first name")]
        public string? FirstName { get; set; }
        [Required(ErrorMessage = "Enter your last name")]
        public string? LastName { get; set; }
        [Required(ErrorMessage = "Selecte Are you Studant or Teacher")]
        public string? StudantOrrTeacher { get; set; }
        [Required(ErrorMessage = "Email address is required.")]
        [EmailAddress(ErrorMessage = "email format should be : Example@email.ceo")]
        public string? Email { get; set; }
        [Required(ErrorMessage = "Password is required.")]
        [DataType(DataType.Password)]
        public string? Password { get; set; }
        [Required(ErrorMessage = "Confirm Password is required.")]
        [DataType(DataType.Password)]
        [Compare("Password",ErrorMessage = "Miss Match")]
        public string? ConfirmPassword { get; set; }
        public Course? course { get; set; }
    }
}
