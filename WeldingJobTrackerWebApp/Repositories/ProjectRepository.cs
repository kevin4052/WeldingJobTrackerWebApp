using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WeldingJobTrackerWebApp.Data;
using WeldingJobTrackerWebApp.Interfaces;
using WeldingJobTrackerWebApp.Models;

namespace WeldingJobTrackerWebApp.Repositories
{
    public class ProjectRepository : IProjectRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IUserRepository _userRepository;

        public ProjectRepository(ApplicationDbContext context, IUserRepository userRepository)
        {
            _context = context;
            _userRepository = userRepository;
        }

        public async Task<IEnumerable<Project>> GetAll()
        {
            var user = await _userRepository.GetCurrentUserAsync();

            return await _context.Projects
                .Include(p => p.ProjectStatus)
                .Where(p => p.CompanyId == user.CompanyId)
                .ToListAsync();
        }

        public async Task<Project> GetByIdAsync(int id)
        {
            var user = await _userRepository.GetCurrentUserAsync();

            var project = await _context.Projects
                .Include(p => p.ProjectStatus)
                .Where(p => p.CompanyId == user.CompanyId)
                .FirstOrDefaultAsync(p => p.Id == id);

            return project!;
        }        

        public async Task<IEnumerable<SelectListItem>> GetSelectItems()
        {
            var projects = await _context.Projects
                .Select(p => new SelectListItem { Value = p.Id.ToString(), Text = p.Name})
                .OrderBy(p => p.Text)
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
