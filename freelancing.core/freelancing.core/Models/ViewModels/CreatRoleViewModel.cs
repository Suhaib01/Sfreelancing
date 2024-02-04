using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VUA.Core.Models.ViewModels
{
    public class CreatRoleViewModel
    {
        [Required]
        public string? RoleName { get; set; }
    }
}
