using Microsoft.AspNetCore.Mvc;
using WeldingJobTrackerWebApp.Interfaces;
using WeldingJobTrackerWebApp.Models;
using WeldingJobTrackerWebApp.ViewModels;

namespace WeldingJobTrackerWebApp.Controllers
{
    public class TeamController : Controller
    {
        private readonly ITeamRepository _teamRepository;

        public TeamController(ITeamRepository teamRepository)
        {
            _teamRepository = teamRepository;
        }

        public async Task<IActionResult> Index()
        {
            var teams = await _teamRepository.GetAll();
            return View(teams);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(TeamViewModel teamViewModel)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "creating client failed");
                return View(teamViewModel);
            }

            var team = new Team
            {
                Name = teamViewModel.Name,
                Projects = teamViewModel.Projects,
                TeamMembers = teamViewModel.TeamMembers,
            };

            _teamRepository.Add(team);

            return RedirectToAction("Index");
        }
    }
}
