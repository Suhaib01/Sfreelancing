using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VUA.Core.Models
{
    public class Course
    {
        public int CourseId { get; set; }
        public string? CourseName { get; set; }
        public string? CourseDescription { get; set; }
        public string? SubDescription { get; set; }
        public string? CourseImage { get; set; }
        public string? CourseDuration { get; set; }
        [Range(1, int.MaxValue)]
        public string? CoursePrice { get; set; }
        public string? InstructorId { get; set; }
        public int CourseStatus { get; set; }
        public ICollection<UserCourse>? UserCourses { get; set; }
		public ICollection<CourseWeeks>? CourseWeeks { get; set; }









	}
}
