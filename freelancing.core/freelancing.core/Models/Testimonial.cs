using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VUA.Core.Models
{
    public class Testimonial
    {
        public int Id { get; set; }
        public string? PersonName { get; set; }
        public string? PersonImage { get; set;}
        public string? PersonMajor { get; set; }
        public string? PersonTestimonial { get; set;}
    }
}
