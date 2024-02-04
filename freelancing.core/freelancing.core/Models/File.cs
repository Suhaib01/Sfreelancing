using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VUA.Core.Models
{
    public class File
    {
        public int FileId { get; set; }
        public string? FileUrl { get; set; }
        public int WeekId { get; set; }
        public Week? Week { get; set; }
    }
}
