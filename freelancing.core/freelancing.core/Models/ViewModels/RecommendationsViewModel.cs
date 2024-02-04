using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace freelancing.core.Models.ViewModels
{
    public class RecommendationsViewModel
    {
        public string? CourseId { get; set; }
        public string? CoursName { get; set; }
        public bool IsSelected { get; set; }
    }
}
