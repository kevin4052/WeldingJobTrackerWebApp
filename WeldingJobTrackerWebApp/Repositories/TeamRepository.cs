﻿using Microsoft.EntityFrameworkCore;
using WeldingJobTrackerWebApp.Data;
using WeldingJobTrackerWebApp.Interfaces;
using WeldingJobTrackerWebApp.Models;

namespace WeldingJobTrackerWebApp.Repositories
{
    public class TeamRepository : ITeamRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public TeamRepository(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<IEnumerable<Team>> GetAll()
        {
            var currentUserId = _httpContextAccessor.HttpContext?.User?.GetUserId();

            return await _context.Teams
                .Include(team => team.Projects)
                .Include(team => team.TeamMembers)
                .Include(team => team.TeamMembers)
                .ToListAsync();
        }

        public async Task<Team> GetByIdAsync(int id)
        {
            return await _context.Teams
                .Include(team => team.Projects)
                .Include(team => team.TeamMembers)
                    .ThenInclude(teamMember => teamMember.User)
                .Include(team => team.TeamMembers)
                    .ThenInclude(teamMember => teamMember.Role)
                .FirstOrDefaultAsync(team => team.Id == id);
        }

        public bool Add(Team team)
        {
            _context.Add(team);
            return Save();
        }

        public bool Delete(Team team)
        {
            _context.Remove(team);
            return Save();
        }

        public bool Save()
        {
            var save = _context.SaveChanges();
            return save > 0;
        }

        public bool Update(Team team)
        {
            _context.Update(team);
            return Save();
        }
    }
}
