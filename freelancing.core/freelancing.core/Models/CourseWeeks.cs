using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VUA.Core.Models
{
	public class CourseWeeks
	{
		public int? CourseId { get; set; }
		public Course? Course { get; set; }
		public int WeekId { get; set; }
		public Week? Week { get; set; }

    }
}
