using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VUA.Core.Models.ViewModels
{
    public class ChangePasswordViweModel
    {
        [Required, DataType(DataType.Password),Display(Name ="Current Password")]
        public string? CurrenPassword { get; set; }
        [Required, DataType(DataType.Password), Display(Name = "New Password")]
        public string? NewPassword { get; set; }
        [Required,Display(Name = "Confirm New Password")]
        [DataType(DataType.Password)]
        [Compare("NewPassword",ErrorMessage ="Confirm New Password does not match")]
        public string? ConfirmNewPassword { get; set; }
    }
}
