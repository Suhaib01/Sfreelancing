using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VUA.Core.Models.ViewModels
{
    public class CompleteProfileModelView
    {
      
        [Required(ErrorMessage = "Date of Birth is required.")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Date of Birth")]
        [Range(typeof(DateTime), "1973-01-01", "2005-01-01", ErrorMessage = "Age must be between 18 and 50.")]
        public DateTime? DateOfBirth { get; set; }
        [Display(Name = "Choose Profile picture")]
        [Required]
        public IFormFile? profilePictuer { get; set; }
        [Required(ErrorMessage = "Please select country.")]
        public string? City { get; set; }
        [Required(ErrorMessage = "Please write your State.")]
        [MinLength(4, ErrorMessage = "Very short state name.")]
        public string? State { get; set; }
        [Required(ErrorMessage = "Please write your Address.")]
        [MinLength(4, ErrorMessage = "Very short Address.")]
        public string? StreetName { get; set; }
        [Required(ErrorMessage = "Postal code is required.")]
        [RegularExpression(@"^\d{5}(-\d{4})?$", ErrorMessage = "Please enter a valid postal code.")]
        public string? PostalCode { get; set; }
        [Required(ErrorMessage = "Select ID Type.")]
        public string? IdType { get; set; }
        [Required(ErrorMessage = "ID Number is required.")]
        [RegularExpression(@"^[A-Z0-9]{8,12}$", ErrorMessage = "Please enter a valid ID Number.")]
        public string? IdNumber { get; set; }



        [Required(ErrorMessage = "ID Expiry Date is required.")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = " ID Expiry Date")]
        [Range(typeof(DateTime), "2023-12-19", "2033-12-31", ErrorMessage = "Invalid Expiry Date", ConvertValueInInvariantCulture = true)]
        public DateTime? ExpiryDate { get; set; }
        [Required(ErrorMessage = "Phone number is required.")]
        [RegularExpression(@"^\d{9,10}$", ErrorMessage = "Please enter a valid phone number.")]
        public string? PhoneNumber { get; set; }
        [Display(Name = "Choose ID picture")]
        [Required]
        public IFormFile? IdFrontSidePictuer { get; set; }
        public IFormFile? IdBackSidePictuer { get; set; }



    }
}
