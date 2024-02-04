using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VUA.Core.Models.ViewModels
{
    public class EditRoleViewModel
    {
        public EditRoleViewModel()
        {
            Users = new List<string>();
        }
        public string? RoleId { get; set; }
        [Required(ErrorMessage ="Enter Role Name")]
        [MinLength(3,ErrorMessage ="Minimum 3 character")]
        [MaxLength(30, ErrorMessage = "Maximum 30 character")]
        public string? RoleName { get; set; }
        public List<string>? Users { get; set; }
    }
}
