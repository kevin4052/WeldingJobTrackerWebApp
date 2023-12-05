using WeldingJobTrackerWebApp.Models;

namespace WeldingJobTrackerWebApp.Interfaces
{
    public interface ITeamRoleRepository
    {
        Task<IEnumerable<TeamRole>> GetTeamRolesAsync();
    }
}
