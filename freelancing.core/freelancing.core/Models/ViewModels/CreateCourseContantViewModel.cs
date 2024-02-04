using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace VUA.Core.Models.ViewModels
{
     public class CreateCourseContantViewModel
    {
        public string? Description { get; set; }
        [Required]
        public string? SubjectName { get; set; }
        [Required]
        public List<IFormFile>? Videos { get; set; }
        [Required]
        public List<IFormFile>? Files { get; set; }

        public string? WhatStudantShouldDo { get; set; }

        public string? EndOfThisWeek { get; set; }
    }
}
