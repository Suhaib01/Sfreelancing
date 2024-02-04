 using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VUA.Core.Models
{
    public class AppllicationUser : IdentityUser
	{
        #region Users
        public string? FullName { get; set; }
        public string? Gender { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string? ProfileImage { get; set; }
        public string? IdType { get; set; }
        public string? IdNumber { get; set; }
        public DateTime? ExpiryDate { get; set; }
        public string? StudantOrTeacher { get; set; }
        public bool? IsCompleted { get; set; }
        public string? IDFrontImage { get; set; }
        public string? IsExternal { get; set; }
        public string? IDbackImage { get; set; }
        public bool IsRejectedUser { get; set; }
        [NotMapped]
        public string? Password { get; set; }
        public bool IsApprovedUser { get; set; }
        
        public ICollection<UserPayment>? UserPayments { get; set; }
        #region AddressReleted
        public string? Address { get; set; }
        public string? Country { get; set; }
        public string? PostalCode { get; set; }
        #endregion
        #region Teacher
        public string? Description { get; set; }
        public string? Major { get; set; }
        public string? CertificateImage { get; set; }
        #endregion
        #endregion
        public ICollection<UserCourse>? UserCourses { get; set; }
        

    }
}
