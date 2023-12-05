using Microsoft.AspNetCore.Mvc.Rendering;
using WeldingJobTrackerWebApp.Models;

namespace WeldingJobTrackerWebApp.Interfaces
{
    public interface IProjectRepository
    {
        Task<IEnumerable<Project>> GetAll();
        Task<IEnumerable<Project>> GetUserProjects(string id);
        Task<IEnumerable<SelectListItem>> GetSelectItems();
        Task<Project> GetByIdAsync(int id);
        bool Add(Project project);
        bool Update(Project project);
        bool Delete(Project project);
        bool Save();
    }
}
