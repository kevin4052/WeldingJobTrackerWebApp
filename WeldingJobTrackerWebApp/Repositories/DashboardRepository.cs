using Microsoft.EntityFrameworkCore;
using WeldingJobTrackerWebApp.Data;
using WeldingJobTrackerWebApp.Interfaces;
using WeldingJobTrackerWebApp.Models;

namespace WeldingJobTrackerWebApp.Repositories
{
    public class DashboardRepository : IDashboardRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IUserRepository _userRepository;

        public DashboardRepository(ApplicationDbContext context, IUserRepository userRepository) 
        { 
            _context = context;
            _userRepository = userRepository;
        }

        public async Task<List<Project>> GetAllUserProjects()
        {
            var userId = _userRepository.GetCurrentUserId();
            var projects = await _context.Projects
                .Include(p => p.Team)
                .ThenInclude(
                    team => team.TeamMembers
                        .Where(teamMember => teamMember.UserId == userId)
                ).ToListAsync();

            return projects;
        }
    }
}
