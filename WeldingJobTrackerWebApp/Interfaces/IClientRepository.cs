using WeldingJobTrackerWebApp.Models;
using static WeldingJobTrackerWebApp.Repositories.ClientRepository;
using static WeldingJobTrackerWebApp.Repositories.UserRepository;

namespace WeldingJobTrackerWebApp.Interfaces
{
    public interface IClientRepository
    {
        Task<IEnumerable<Client>> GetAll();
        Task<IEnumerable<ClientNameId>> GetAllClientNameId();
        Task<Client> GetByIdAsync(int id);
        Task<Client> GetByIdAsyncNoTracking(int id);
        bool Add(Client client);
        bool Update(Client client);
        bool Delete(Client client);
        bool Save();
    }
}
