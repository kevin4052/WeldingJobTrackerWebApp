﻿using Microsoft.AspNetCore.Mvc.Rendering;
using WeldingJobTrackerWebApp.Models;

namespace WeldingJobTrackerWebApp.Interfaces
{
    public interface IClientRepository
    {
        Task<IEnumerable<Client>> GetAll();
        Task<IEnumerable<SelectListItem>> GetSelectItems();
        Task<Client> GetByIdAsync(int id);
        Task<Client> GetByIdAsyncNoTracking(int id);
        bool Add(Client client);
        bool Update(Client client);
        bool Delete(Client client);
        bool Save();
    }
}
