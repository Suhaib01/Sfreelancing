using VUA.Core.Models;

namespace VUA.EF.Repositories
{
    public interface IDashboardRipository
    {
        IEnumerable<Course> Search(string term);
    }
}