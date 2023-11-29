using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WeldingJobTrackerWebApp.Interfaces;
using WeldingJobTrackerWebApp.Models;
using WeldingJobTrackerWebApp.ViewModels;

namespace WeldingJobTrackerWebApp.Controllers
{
    public class TeamController : Controller
    {
        private readonly ITeamRepository _teamRepository;
        private readonly IProjectRepository _projectRepository;
        private readonly IUserRepository _userRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<User> _userManager;

        public TeamController(
            ITeamRepository teamRepository, 
            IProjectRepository projectRepository, 
            IUserRepository userRepository,
            IHttpContextAccessor httpContextAccessor,
            UserManager<User> userManager)
        {
            _teamRepository = teamRepository;
            _projectRepository = projectRepository;
            _userRepository = userRepository;
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var teams = await _teamRepository.GetAll();
            return View(teams);
        }

        public async Task<IActionResult> Create()
        {
            var projectSelectList = await _projectRepository.GetSelectItems();
            var userSelectGroups = await _userRepository.GetSelectItems();
            var currentUserId = _httpContextAccessor.HttpContext?.User?.GetUserId();
            var currentUser = await _userManager.FindByIdAsync(currentUserId);

            var teamViewModel = new TeamViewModel()
            {
                CompanyId = currentUser.CompanyId,
                ProjectSelectList = new List<SelectListItem>(),
                AdminSelectList = new List<SelectListItem>(),
                WelderSelectList = new List<SelectListItem>(),
            };

            foreach (var project in projectSelectList)
            {
                teamViewModel.ProjectSelectList.Add(new SelectListItem
                {
                    Text = project.Name,
                    Value = project.Id.ToString()
                });
            }

            foreach (var userListGroup in userSelectGroups) 
            { 
                if (userListGroup.Role == "welder")
                {
                    teamViewModel.WelderSelectList.AddRange(userListGroup.Users);
                } else
                {
                    teamViewModel.AdminSelectList.AddRange(userListGroup.Users);
                }
            }

            return View(teamViewModel);
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
                Projects = new List<Project>(),
                TeamMembers = new List<TeamMember>(),
            };

            _teamRepository.Add(team);

            return RedirectToAction("Index");
        }
    }
}
