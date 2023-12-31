﻿using Microsoft.AspNetCore.Mvc.Rendering;
using WeldingJobTrackerWebApp.Models;

namespace WeldingJobTrackerWebApp.Interfaces
{
    public interface ITeamRepository
    {
        Task<Team> GetByIdAsync(int id);
        Task<Team> GetByIdAsyncNoTracking(int id);
        Task<IEnumerable<Team>> GetAll();
        Task<IEnumerable<Team>> GetUserTeams(string id);
        Task<IEnumerable<SelectListItem>> GetSelectItems();
        bool Add(Team team);
        bool Update(Team team);
        bool Delete(Team team);
        bool Save();
    }
}
