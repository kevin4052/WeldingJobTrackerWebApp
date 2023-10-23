using Microsoft.EntityFrameworkCore;
using WeldingJobTrackerWebApp.Data;
using WeldingJobTrackerWebApp.Interfaces;
using WeldingJobTrackerWebApp.Models;

namespace WeldingJobTrackerWebApp.Repositories
{
    public class ClientRepository : IClientRepository
    {
        private readonly ApplicationDbContext _context;

        public ClientRepository(ApplicationDbContext context)
        {
            _context = context;
        }


        public bool Add(Client client)
        {
            _context.Add(client);
            return Save();
        }

        public bool Delete(Client client)
        {
            _context.Remove(client);
            return Save();
        }

        public async Task<IEnumerable<Client>> GetAll()
        {
            return await _context.Clients.Include(c => c.Address).ToListAsync();
        }

        public async Task<Client> GetByIdAsync(int id)
        {
            return await _context.Clients.Include(c => c.Address).FirstOrDefaultAsync(c => c.Id == id);
        }

        public bool Save()
        {
            var save = _context.SaveChanges();
            return save > 0;
        }

        public bool Update(Client client)
        {
            _context.Update(client);
            return Save();
        }
    }
}
