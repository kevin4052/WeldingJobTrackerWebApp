using Microsoft.EntityFrameworkCore;
using WeldingJobTrackerWebApp.Data;
using WeldingJobTrackerWebApp.Interfaces;
using WeldingJobTrackerWebApp.Models;

namespace WeldingJobTrackerWebApp.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext applicationDbContext)
        {
            _context = applicationDbContext;
        }

        public class UserNameAndId
        {
            public string Id { get; set; }
            public string Name { get; set; }
        }

        public async Task<IEnumerable<UserNameAndId>> GetAllUsersNameId()
        {
            var users = await _context.Users
                .Select(u => new UserNameAndId { Id = u.Id, Name = u.LastName + ", " + u.FirstName })
                .OrderBy(u => u.Name)
                .ToListAsync();
            return users;
        }

        public async Task<User> GetUserbyIdAsync(string id)
        {
            //return await _context.Users
            //    .Include(u => u.Projects)
            //    .FirstOrDefaultAsync(u => u.Id == id);
            throw new NotImplementedException();
        }
    }
}
