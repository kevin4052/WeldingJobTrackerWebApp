using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WeldingJobTrackerWebApp.Data;
using WeldingJobTrackerWebApp.Interfaces;

namespace WeldingJobTrackerWebApp.Repositories
{
    public class RoleRepository : IRoleRepository
    {
        private readonly ApplicationDbContext _context;

        public RoleRepository(ApplicationDbContext applicationDbContext)
        {
            _context = applicationDbContext;
        }

        public async Task<List<SelectListItem>> GetAllSelectRoles()
        {
            var roles = await _context.Roles
                .Select(x => new SelectListItem { Value = x.Id, Text = x.Name })
                .OrderBy(x => x.Text)
                .ToListAsync();

            return roles;
        }
    }
}
