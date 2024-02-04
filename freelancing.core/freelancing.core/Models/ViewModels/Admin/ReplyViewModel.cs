using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VUA.Core.Models.ViewModels.Admin
{
    public class ReplyViewModel
    {
        public int Id { get; set; }
        [Required]
        public string? ReplyMessage { get; set; }
    }
}
