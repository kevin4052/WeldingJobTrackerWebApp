using Microsoft.AspNetCore.Identity;
using WeldingJobTrackerWebApp.Models;
using static WeldingJobTrackerWebApp.Repositories.UserRepository;

namespace WeldingJobTrackerWebApp.Interfaces
{
    public interface IUserRepository
    {
        string GetCurrentUserId();
        Task<User> GetCurrentUserAsync();
        Task<User> GetUserbyIdAsync(string id);
        Task<IdentityResult> Update(User user);
        Task<IEnumerable<UserRoleGroup>> GetSelectItems();
    }
}
