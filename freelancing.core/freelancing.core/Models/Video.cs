

namespace VUA.Core.Models
{
    public class Video
    {
        public int VideoId { get; set; }
        public string? VideoUrl { get; set; }
        public int WeekId { get; set; }
        public Week? Week { get; set; }
    }
}
