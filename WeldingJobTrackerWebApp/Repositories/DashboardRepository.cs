using Microsoft.EntityFrameworkCore;
using WeldingJobTrackerWebApp.Data;
using WeldingJobTrackerWebApp.Interfaces;
using WeldingJobTrackerWebApp.Models;

namespace WeldingJobTrackerWebApp.Repositories
{
    public class DashboardRepository : IDashboardRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public DashboardRepository(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor) 
        { 
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<List<Project>> GetAllUserProjects()
        {
            var currentUser = _httpContextAccessor.HttpContext?.User;
            var projects = await _context.Projects
                .Include(p => p.Client)
                .Include(p => p.ProjectStatus)
                .Where(p => p.Equals(currentUser))
                .ToListAsync();
            return projects;
        }
    }
}
