﻿using WeldingJobTrackerWebApp.Models;

namespace WeldingJobTrackerWebApp.Interfaces
{
    public interface IProjectStatusRepository
    {
        Task<IEnumerable<ProjectStatus>> GetAll();
        Task<ProjectStatus> GetByIdAsync(int id);
        Task<ProjectStatus> GetByCodeAsync(string code);
        bool Add(ProjectStatus project);
        bool Update(ProjectStatus project);
        bool Delete(ProjectStatus project);
        bool Save();
    }
}
