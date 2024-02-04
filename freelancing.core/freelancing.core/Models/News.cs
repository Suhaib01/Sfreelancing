using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VUA.Core.Models
{
    public class News
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Sector { get; set; }
        public string? Image { get; set; }
        public DateTime Date { get; set; }

    }
}
