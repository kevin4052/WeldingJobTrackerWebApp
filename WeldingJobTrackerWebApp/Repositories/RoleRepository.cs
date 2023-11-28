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

        public class SelectRoles
        {
            public string Id { get; set; }
            public string Name { get; set; }
        }

        public async Task<IEnumerable<SelectRoles>> GetAllSelectRoles()
        {
            var roles = await _context.Roles
                .Select(x => new SelectRoles { Id = x.Id, Name = x.Name })
                .OrderBy(x => x.Name)
                .ToListAsync();

            return roles;
        }
    }
}
