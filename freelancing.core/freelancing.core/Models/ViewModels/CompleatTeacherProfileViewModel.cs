using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace VUA.Core.Models.ViewModels
{
    public class CompleatTeacherProfileViewModel : CompleteProfileModelView
    {
        //[Display(Name = "Choose Certificate picture")]
        //[Required]
        public IFormFile? CertificateImage { get; set; }
        [Required]
        [Display(Name = "Write about yourself:")]
        public string? Description { get; set; }
        [Required]
        [Display(Name = "The field related to your services?")]
        public string? Major { get; set; }
       
    }
}
