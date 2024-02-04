using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using VUA.Core.Models;
using VUA.Core.Models.ViewModels;

namespace VUA.EF.Repositories
{
    public interface IAccountRepositorory
    {
        //Task<IdentityResult> CreateUserAsync(RegisterViewModel model);
        Task<string> UploadPowerPoint(IFormFile powerPointFile, string Web);
        Task SignOutAcync();
        Task<AppllicationUser> FindUserByIdAsync(string id);
        Task<AppllicationUser> FindUserByEmailAsync(string email);
        Task<bool> IsInRoleAsync(AppllicationUser user, string RoleName);
        Task<SignInResult> PasswordUserSignInAsync(LoginViewModel model);
        Task<AppllicationUser> GetApplicationUser();
        Task<IdentityResult> UpdateUserAsync(AppllicationUser user);
        Task<IdentityResult> CompleteUserProfile(CompleteProfileModelView model, string WRP);
        Task<IdentityResult> CompleteTeacherUserProfile(CompleatTeacherProfileViewModel model, string WRP);
        Task<string> UploadImag(string folderPath, IFormFile file, string WebRootPath);
        Task<string> UploadVideo(IFormFile model);
        void AddCourse(int courseid);
        Task<IdentityResult> AddUserToRoleAsync(AppllicationUser Studant,string roleName);
        Task<AppllicationUser> GetApplicationUser(ClaimsPrincipal user);
        AppllicationUser IsSignInUser(ClaimsPrincipal user);
        bool ChekPhoneNumper(string phone);
        Task<IdentityResult> ChangePasswordAsync(ChangePasswordViweModel model);
    }
}