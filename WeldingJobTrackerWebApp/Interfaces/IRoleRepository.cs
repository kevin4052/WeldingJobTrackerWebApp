using static WeldingJobTrackerWebApp.Repositories.RoleRepository;
using static WeldingJobTrackerWebApp.Repositories.UserRepository;

namespace WeldingJobTrackerWebApp.Interfaces
{
    public interface IRoleRepository
    {
        Task<IEnumerable<SelectRoles>> GetAllSelectRoles();
    }
}
