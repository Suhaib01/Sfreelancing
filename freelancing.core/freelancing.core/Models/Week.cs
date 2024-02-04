using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VUA.Core.Models
{
	public class Week
	{
	
        public int WeekId { get; set; }

        public string? Description { get; set; }

        public string? SubjectName { get; set; }
        public string? WhatStudantShouldDo { get; set; }
        public string? EndOfThisWeek { get; set; }

        // Navigation properties
        public ICollection<CourseWeeks>? CourseWeeks { get; set; }
        public ICollection<Video>? Videos { get; set; }
        public ICollection<File>? Files { get; set; }

    }
}
