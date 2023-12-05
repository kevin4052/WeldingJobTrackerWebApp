using WeldingJobTrackerWebApp.Models;
using static WeldingJobTrackerWebApp.Repositories.UserRepository;

namespace WeldingJobTrackerWebApp.Interfaces
{
    public interface IUserRepository
    {
        string GetCurrentUserId();
        Task<User> GetCurrentUserAsync();
        Task<User> GetUserbyIdAsync(string id);
        Task<IEnumerable<UserRoleGroup>> GetSelectItems();
    }
}
