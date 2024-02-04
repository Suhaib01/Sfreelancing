using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VUA.Core.Models.ViewModels.Admin
{
    public class CreateCourseViewModel
    {
       
        [Required]
        public string? CourseName { get; set; }
        [Required]
        public string? CourseDescription { get; set; }
        [Required]
        public string? SubDescription { get; set; }
        [Required]
        public IFormFile? CourseImage { get; set; }
        [Required]
        public string? CourseDuration { get; set; }
        [Required]
        [Range(1, int.MaxValue)]
        public string? CoursePrice { get; set; }
    }
}
