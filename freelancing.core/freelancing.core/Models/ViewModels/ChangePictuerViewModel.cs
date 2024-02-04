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
    public class ChangePictuerViewModel
    {
        [Display(Name = "Choose Profile picture")]
        [Required]
        public IFormFile? profilePictuer { get; set; }
    }
}
