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

        public UserRepository(ApplicationDbContext applicationDbContext)
        {
            _context = applicationDbContext;
        }

        public class UserRoleGroup
        {
            public string Role { get; set; }
            public List<SelectListItem> Users { get; set; }
        }

        public class UserSelectItem
        {
            public string Id { get; set; }  
            public string Name { get; set; }
        }

        public async Task<IEnumerable<UserRoleGroup>> GetSelectItems()
        {
            var userSelectList = (
                from user in _context.Users
                join userRole in _context.UserRoles on user.Id equals userRole.UserId
                join role in _context.Roles on userRole.RoleId equals role.Id
                select new
                {
                    user.Id,
                    Name = user.LastName + ", " + user.FirstName,
                    Role = role.Name,
                }
            );

            var adminList = new UserRoleGroup()
            {
                Role = "admin",
                Users = new List<SelectListItem>()
            };
            var userList = new UserRoleGroup()
            {
                Role = "user",
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
                    adminList.Users.Add(new SelectListItem { Value = user.Id, Text = user.Name});
                }
                if (user.Role == "user")
                {
                    userList.Users.Add(new SelectListItem { Value = user.Id, Text = user.Name });
                }
                if (user.Role == "welder")
                {
                    welderList.Users.Add(new SelectListItem { Value = user.Id, Text = user.Name });
                }
            }

            var userRoleList = new List<UserRoleGroup>()
            {
                adminList,
                userList,
                welderList
            };

            return userRoleList;
        }

        public async Task<User> GetUserbyIdAsync(string id)
        {
            return await _context.Users
                .Include(u => u.Company)
                .FirstOrDefaultAsync(u => u.Id == id);
        }
    }
}
