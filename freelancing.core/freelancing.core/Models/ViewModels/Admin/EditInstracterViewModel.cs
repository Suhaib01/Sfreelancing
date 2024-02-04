using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VUA.Core.Models.ViewModels.Admin
{
    public class EditInstracterViewModel
    {
        public EditInstracterViewModel()
        {
            Instractors = new List<string>();
        }
        public int? CourseId { get; set; }
        
        public string? CoursName { get; set; }
        public List<string>? Instractors { get; set; }
    }
}
