using WeldingJobTrackerWebApp.Models;

namespace WeldingJobTrackerWebApp.Interfaces
{
    public interface IUserRepository
    {
        Task<User> GetUserbyIdAsync(string id);
    }
}
