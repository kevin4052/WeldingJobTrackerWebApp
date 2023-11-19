using WeldingJobTrackerWebApp.Data;
using WeldingJobTrackerWebApp.Interfaces;
using WeldingJobTrackerWebApp.Models;

namespace WeldingJobTrackerWebApp.Repositories
{
    public class DashboardRepository : IDashboardRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUserRepository _userRepository;

        public DashboardRepository(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor, IUserRepository userRepository) 
        { 
            _context = context;
            _httpContextAccessor = httpContextAccessor;
            _userRepository = userRepository;
        }

        public async Task<List<Project>> GetAllUserProjects()
        {
            var currentUserId = _httpContextAccessor.HttpContext?.User?.GetUserId();
            var user = await _userRepository.GetUserbyIdAsync(currentUserId);
            return user.Projects.ToList();
        }
    }
}
