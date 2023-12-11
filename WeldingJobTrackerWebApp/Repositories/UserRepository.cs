using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WeldingJobTrackerWebApp.Data;
using WeldingJobTrackerWebApp.Interfaces;
using WeldingJobTrackerWebApp.Models;

namespace WeldingJobTrackerWebApp.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<User> _userManager;

        public UserRepository(ApplicationDbContext applicationDbContext, IHttpContextAccessor httpContextAccessor, UserManager<User> userManager)
        {
            _context = applicationDbContext;
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
        }

        public class UserRoleGroup
        {
            public string Role { get; set; }
            public List<SelectListItem> Users { get; set; }
        }

        public string GetCurrentUserId()
        {
            return _httpContextAccessor.HttpContext?.User?.GetUserId();
        }

        public async Task<User> GetCurrentUserAsync()
        {
            var currentUserId = this.GetCurrentUserId();
            return await _userManager.FindByIdAsync(currentUserId);
        }

        public async Task<IEnumerable<UserRoleGroup>> GetSelectItems()
        {
            var userSelectList = await (
                from user in _context.Users
                join userRole in _context.UserRoles on user.Id equals userRole.UserId
                join role in _context.Roles on userRole.RoleId equals role.Id
                select new
                {
                    user.Id,
                    Name = user.LastName + ", " + user.FirstName,
                    Role = role.Name,
                }
            ).ToListAsync();

            var adminList = new UserRoleGroup()
            {
                Role = "admin",
                Users = new List<SelectListItem>()
            };

            var welderList = new UserRoleGroup()
            {
                Role = "welder",
                Users = new List<SelectListItem>()
            };

            foreach ( var user in userSelectList )
            {
                if ( user.Role == "admin" ) 
                {
                    adminList.Users.Add(new SelectListItem { Value = user.Id, Text = user.Name });
                }

                if (user.Role == "welder")
                {
                    welderList.Users.Add(new SelectListItem { Value = user.Id, Text = user.Name });
                }
            }

            var userRoleList = new List<UserRoleGroup>()
            {
                adminList,
                welderList
            };

            return userRoleList;
        }

        public async Task<User> GetUserbyIdAsync(string id)
        {
            return await _userManager.Users
                .Include(u => u.Company)
                .Include(u => u.Address)
                .Include(u => u.Image)
                .FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<IdentityResult> Update(User user)
        {
            return await _userManager.UpdateAsync(user);
        }

    }
}
