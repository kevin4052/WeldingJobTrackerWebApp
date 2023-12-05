using Microsoft.AspNetCore.Mvc.Rendering;
using static WeldingJobTrackerWebApp.Repositories.RoleRepository;
using static WeldingJobTrackerWebApp.Repositories.UserRepository;

namespace WeldingJobTrackerWebApp.Interfaces
{
    public interface IRoleRepository
    {
        Task<List<SelectListItem>> GetAllSelectRoles();
    }
}
