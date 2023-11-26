using Microsoft.EntityFrameworkCore;
using WeldingJobTrackerWebApp.Data;
using WeldingJobTrackerWebApp.Interfaces;
using WeldingJobTrackerWebApp.Models;

namespace WeldingJobTrackerWebApp.Repositories
{
    public class ProjectRepository : IProjectRepository
    {
        private readonly ApplicationDbContext _context;

        public ProjectRepository(ApplicationDbContext context)
        {
            _context = context;
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

        public async Task<IEnumerable<Project>> GetAll()
        {
            //return await _context.Projects
            //    .Include(p => p.Client)
            //    .Include(p => p.ProjectStatus)
            //    .Include(p => p.UserMembers)
            //    .ToListAsync();
            throw new NotImplementedException();
        }

        public async Task<Project> GetByIdAsync(int id)
        {
            //var project = await _context.Projects
            //    .Include(p => p.Client)
            //    .Include(p => p.ProjectStatus)
            //    .Include(p => p.UserMembers)
            //    .FirstOrDefaultAsync(p => p.Id == id);

            //return project!;
            throw new NotImplementedException();
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
