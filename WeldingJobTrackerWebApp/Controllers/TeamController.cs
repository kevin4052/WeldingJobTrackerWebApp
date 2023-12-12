using Microsoft.AspNetCore.Mvc;
using WeldingJobTrackerWebApp.Interfaces;
using WeldingJobTrackerWebApp.Models;
using WeldingJobTrackerWebApp.Models.ViewModels.ViewTeam;
using WeldingJobTrackerWebApp.Data;
using WeldingJobTrackerWebApp.Models.ViewModels;

namespace WeldingJobTrackerWebApp.Controllers
{
    public class TeamController : Controller
    {
        private readonly ITeamRepository _teamRepository;
        private readonly ITeamRoleRepository _teamRoleRepository;
        private readonly IProjectRepository _projectRepository;
        private readonly IUserRepository _userRepository;

        public TeamController(
            ITeamRepository teamRepository, 
            ITeamRoleRepository teamRoleRepository,
            IProjectRepository projectRepository, 
            IUserRepository userRepository)
        {
            _teamRepository = teamRepository;
            _teamRoleRepository = teamRoleRepository;
            _projectRepository = projectRepository;
            _userRepository = userRepository;
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
            var currentUser = await _userRepository.GetCurrentUserAsync();

            var teamViewModel = new CreateTeamViewModel()
            {
                CompanyId = currentUser.CompanyId,
                ProjectSelectList = projectSelectList.ToList(),
                AdminSelectList = userSelectGroups.FirstOrDefault(g => g.Role == "admin").Users,
                WelderSelectList = userSelectGroups.FirstOrDefault(g => g.Role == "welder").Users,
            };

            return View(teamViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateTeamViewModel teamViewModel)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "creating client failed");
                return View(teamViewModel);
            }

            var teamRoles = await _teamRoleRepository.GetTeamRolesAsync();

            var team = new Team
            {
                Name = teamViewModel.Name,
                Projects = new List<Project>(),
                TeamMembers = new List<TeamMember>()
                {
                    new TeamMember
                    {
                        UserId = teamViewModel.SelectedWelderId,
                        RoleId = teamRoles.FirstOrDefault(r => r.Code == TeamRoleCode.Labor).Id
                    },
                    new TeamMember
                    {
                        UserId = teamViewModel.SelectedAdminId,
                        RoleId = teamRoles.FirstOrDefault(r => r.Code == TeamRoleCode.ProjectManager).Id
                    }
                },
            };

            _teamRepository.Add(team);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Detail(int id)
        {
            var team = await _teamRepository.GetByIdAsync(id);

            var teamViewModel = new DetailTeamViewModel
            {
                Id = id,
                Name = team.Name,
                Admin = team.TeamMembers.FirstOrDefault(tm => tm.Role.Code == TeamRoleCode.ProjectManager).User,
                Welder = team.TeamMembers.FirstOrDefault(tm => tm.Role.Code == TeamRoleCode.Labor).User
            };

            return View(teamViewModel);
        }

        public async Task<IActionResult> Edit(int id)
        {
            if (id == 0)
            {
                RedirectToAction("Index");
            }

            var projectSelectList = await _projectRepository.GetSelectItems();
            var userSelectGroups = await _userRepository.GetSelectItems();
            var currentUser = await _userRepository.GetCurrentUserAsync();

            var team = await _teamRepository.GetByIdAsync(id);

            var teamViewModel = new EditTeamViewModel()
            {
                CompanyId = currentUser.CompanyId,
                Name = team.Name,
                ProjectSelectList = projectSelectList.ToList(),
                AdminSelectList = userSelectGroups.FirstOrDefault(g => g.Role == "admin").Users,
                WelderSelectList = userSelectGroups.FirstOrDefault(g => g.Role == "welder").Users,
                SelectedAdminId = team.TeamMembers.FirstOrDefault(tm => tm.Role.Code == TeamRoleCode.ProjectManager).UserId,
                SelectedWelderId = team.TeamMembers.FirstOrDefault(tm => tm.Role.Code == TeamRoleCode.Labor).UserId,
                SelectedProjectId = team.Projects.First()?.Id ?? 0,
                Test = new List<CheckBoxOption>()
                {
                    new CheckBoxOption()
                    {
                        IsChecked = false,
                        Text = "test 1",
                        value = "1"
                    },
                    new CheckBoxOption()
                    {
                        IsChecked= false,
                        Text = "test 2",
                        value = "2"
                    },
                    new CheckBoxOption()
                    {
                        IsChecked = false,
                        Text = "test 3",
                        value = "3"
                    }
                }
            };

            return View(teamViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, EditTeamViewModel teamViewModel)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "creating client failed");
                return View(teamViewModel);
            }

            var team = await _teamRepository.GetByIdAsyncNoTracking(id);

            if (team == null)
            {
                return View("Error");
            }

            var teamRoles = await _teamRoleRepository.GetTeamRolesAsync();

            var updatedteam = new Team
            {
                Name = teamViewModel.Name,
                Projects = new List<Project>(),
                TeamMembers = new List<TeamMember>()
                {
                    new TeamMember
                    {
                        UserId = teamViewModel.SelectedWelderId,
                        RoleId = teamRoles.FirstOrDefault(r => r.Code == TeamRoleCode.Labor).Id
                    },
                    new TeamMember
                    {
                        UserId = teamViewModel.SelectedAdminId,
                        RoleId = teamRoles.FirstOrDefault(r => r.Code == TeamRoleCode.ProjectManager).Id
                    }
                },
            };

            _teamRepository.Update(updatedteam);

            return RedirectToAction("Index");
        }
    }
}
