using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;
using WeldingJobTrackerWebApp.Data;
using WeldingJobTrackerWebApp.Interfaces;
using WeldingJobTrackerWebApp.Models;

namespace WeldingJobTrackerWebApp.Repositories
{
    public class ProjectStatusRepository : IProjectStatusRepository
    {
        private readonly ApplicationDbContext _context;

        public ProjectStatusRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public bool Add(ProjectStatus projectStatus)
        {
            _context.Add(projectStatus);
            return Save();
        }

        public bool Delete(ProjectStatus projectStatus)
        {
            _context.Remove(projectStatus);
            return Save();
        }

        public async Task<IEnumerable<ProjectStatus>> GetAll()
        {
            return await _context.ProjectStatuses.ToListAsync();
        }

        public async Task<ProjectStatus> GetByIdAsync(int id)
        {
            var projectStatus = await _context.ProjectStatuses.FirstOrDefaultAsync(ps => ps.Id == id);
            
            return projectStatus!;
        }

        public async Task<ProjectStatus> GetByCodeAsync(string code)
        {
            var projectStatus = await _context.ProjectStatuses.FirstOrDefaultAsync(ps => ps.Code == code);

            return projectStatus!;
        }

        public bool Save()
        {
            var save = _context.SaveChanges();
            return save > 0;
        }

        public bool Update(ProjectStatus projectStatus)
        {
            _context.Update(projectStatus);
            return Save();
        }
    }
}
