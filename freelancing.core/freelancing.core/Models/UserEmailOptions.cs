using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VUA.Core.Models
{
    public class UserEmailOptions
    {
        public List<KeyValuePair<string, string>>?PlaceHolders;

        public List<string>? ToEmails { get; set; }
        public string? Subject { get; set; }
        public string? Body { get; set; }
    }
}
