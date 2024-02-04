using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using VUA.Core.Models;

namespace VUA.Core.IRepositories.IService
{
    public interface IUserService
    {
        string GetUserId();
        IEnumerable<Course> GetCourses();
    }
}