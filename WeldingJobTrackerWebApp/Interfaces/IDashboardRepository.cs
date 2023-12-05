using WeldingJobTrackerWebApp.Models;

namespace WeldingJobTrackerWebApp.Interfaces
{
    public interface IDashboardRepository
    {
        Task<List<Project>> GetAllUserProjects();
    }
}
