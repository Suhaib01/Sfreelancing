
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VUA.Core.Models
{
    public class UserCourse
    {
        public string? UserId { get; set; }
        public AppllicationUser? User { get; set; }
        public int CourseId { get; set; }
        public Course? Course { get; set; }
        

    }
}
