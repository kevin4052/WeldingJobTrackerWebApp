using Microsoft.EntityFrameworkCore;
using WeldingJobTrackerWebApp.Data;
using WeldingJobTrackerWebApp.Interfaces;
using WeldingJobTrackerWebApp.Models;

namespace WeldingJobTrackerWebApp.Repositories
{
    public class ProjectRepository : IProjectRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUserRepository _userRepository;

        public ProjectRepository(
            ApplicationDbContext context, 
            IHttpContextAccessor httpContextAccessor, 
            IUserRepository userRepository)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
            _userRepository = userRepository;
        }

        public class ProjectSelectItem
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }

        public async Task<IEnumerable<Project>> GetAll()
        {
            var currentUserId = _httpContextAccessor.HttpContext?.User?.GetUserId();
            var user = await _userRepository.GetUserbyIdAsync(currentUserId);

            return await _context.Projects
                .Include(p => p.ProjectStatus)
                .Where(p => p.CompanyId == user.CompanyId)
                .ToListAsync();
        }

        public async Task<Project> GetByIdAsync(int id)
        {
            var currentUserId = _httpContextAccessor.HttpContext?.User?.GetUserId();
            var user = await _userRepository.GetUserbyIdAsync(currentUserId);

            var project = await _context.Projects
                .Include(p => p.ProjectStatus)
                .Where(p => p.CompanyId == user.CompanyId)
                .FirstOrDefaultAsync(p => p.Id == id);

            return project!;
        }        

        public async Task<IEnumerable<ProjectSelectItem>> GetSelectItems()
        {
            var projects = await _context.Projects
                .Select(p => new ProjectSelectItem { Id = p.Id, Name = p.Name})
                .OrderBy(p => p.Name)
                .ToListAsync();
            return projects;
        }

        public bool Add(Project project)
        {
            _context.Add(project);
            return Save();
        }

        public bool Delete(Project project)
        {
            _context.Remove(project);
            return Save();
        }

        public bool Save()
        {
            var save = _context.SaveChanges();
            return save > 0;
        }

        public bool Update(Project project)
        {
            _context.Update(project);
            return Save();
        }
    }
}
