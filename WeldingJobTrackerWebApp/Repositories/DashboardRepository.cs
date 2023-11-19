using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
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
            var currentUserId = _httpContextAccessor.HttpContext?.User?.GetUserId();
            var projects = await _context.Projects
                .Include(p => p.Client)
                .Include(p => p.ProjectStatus)
                .Where(p => p.Members.Any(m => m.Id == currentUserId))
                .ToListAsync();
            return projects;
        }
    }
}
