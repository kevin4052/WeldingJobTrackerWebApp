﻿using Microsoft.EntityFrameworkCore;
using WeldingJobTrackerWebApp.Data;
using WeldingJobTrackerWebApp.Interfaces;
using WeldingJobTrackerWebApp.Models;
using static WeldingJobTrackerWebApp.Repositories.UserRepository;

namespace WeldingJobTrackerWebApp.Repositories
{
    public class ClientRepository : IClientRepository
    {
        private readonly ApplicationDbContext _context;

        public ClientRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public class ClientNameId
        {
            public int Id { get; set; }
            public string Name { get; set; }
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
            return await _context.Clients
                .Include(c => c.Address)
                .Include(c => c.Image)
                .ToListAsync();
        }

        public async Task<IEnumerable<ClientNameId>> GetAllClientNameId()
        {
            var clients = await _context.Clients
                .Select(u => new ClientNameId { Id = u.Id, Name = u.Name })
                .OrderBy(u => u.Name)
                .ToListAsync();
            return clients;
        }

        public async Task<Client> GetByIdAsync(int id)
        {
            var client = await _context.Clients
                .Include(c => c.Address)
                .Include(c => c.Image)
                .FirstOrDefaultAsync(c => c.Id == id);

            return client!;
        }

        public async Task<Client> GetByIdAsyncNoTracking(int id)
        {
            var client = await _context.Clients
                .Include(c => c.Address)
                .Include(c => c.Image)
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.Id == id);

            return client!;
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
