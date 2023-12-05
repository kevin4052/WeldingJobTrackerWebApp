using Microsoft.EntityFrameworkCore;
using WeldingJobTrackerWebApp.Data;
using WeldingJobTrackerWebApp.Interfaces;
using WeldingJobTrackerWebApp.Models;

namespace WeldingJobTrackerWebApp.Repositories
{
    public class TeamRoleRepository : ITeamRoleRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public TeamRoleRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<IEnumerable<TeamRole>> GetTeamRolesAsync()
        {
            return await _applicationDbContext.TeamRoles.ToArrayAsync();
        }
    }
}
