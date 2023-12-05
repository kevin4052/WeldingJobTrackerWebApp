using Microsoft.AspNetCore.Mvc;
using WeldingJobTrackerWebApp.Interfaces;
using WeldingJobTrackerWebApp.Models;
using WeldingJobTrackerWebApp.Models.ViewModels.ViewProject;

namespace WeldingJobTrackerWebApp.Controllers
{
    public class ProjectController : Controller
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IProjectStatusRepository _projectStatusRepository;
        private readonly IUserRepository _userRepository;
        private readonly ITeamRepository _teamRepository;

        public ProjectController(
            IProjectRepository projectRepository, 
            IProjectStatusRepository projectStatusRepository, 
            IUserRepository userRepository, 
            ITeamRepository teamRepository)
        {
            _projectRepository = projectRepository;
            _projectStatusRepository = projectStatusRepository;
            _userRepository = userRepository;
            _teamRepository = teamRepository;
        }
        public async Task<IActionResult> Index()
        {
            var projects = await _projectRepository.GetAll();
            return View(projects);
        }

        public async Task<IActionResult> Detail(int id)
        {
            var project = await _projectRepository.GetByIdAsync(id);
            return View(project);
        }

        public async Task<IActionResult> Create() 
        {
            var projectStatusSelectList = await _projectStatusRepository.GetSelectItems();
            var teamSelectList = await _teamRepository.GetSelectItems();
            var currentUser = await _userRepository.GetCurrentUserAsync();

            var projectViewModel = new CreateProjectViewModel()
            {
                CompanyId = currentUser.CompanyId,
                ProjectStatusSelectList = projectStatusSelectList.ToList(),
                TeamSelectList = teamSelectList.ToList(),
            };

            return View(projectViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateProjectViewModel projectViewModel)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Project save failed");
                return View(projectViewModel);
            }

            var project = new Project
            {
                Name = projectViewModel.Name,
                CompanyId = projectViewModel.CompanyId,
                ProjectStatusId = projectViewModel.SelectedProjectStatusId,
                TeamId = projectViewModel.SelectedTeamId
            };


            _projectRepository.Add(project);
            return RedirectToAction("Index");
        }
    }
}
