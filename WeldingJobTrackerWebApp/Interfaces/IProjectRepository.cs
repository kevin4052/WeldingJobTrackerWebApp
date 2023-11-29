using WeldingJobTrackerWebApp.Models;
using static WeldingJobTrackerWebApp.Repositories.ProjectRepository;

namespace WeldingJobTrackerWebApp.Interfaces
{
    public interface IProjectRepository
    {
        Task<IEnumerable<Project>> GetAll();
        Task<IEnumerable<ProjectSelectItem>> GetSelectItems();
        Task<Project> GetByIdAsync(int id);
        bool Add(Project project);
        bool Update(Project project);
        bool Delete(Project project);
        bool Save();
    }
}
