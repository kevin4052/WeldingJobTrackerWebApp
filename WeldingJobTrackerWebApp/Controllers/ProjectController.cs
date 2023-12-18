using Microsoft.AspNetCore.Mvc;
using WeldingJobTrackerWebApp.Data;
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
        public IActionResult Create(CreateProjectViewModel projectViewModel)
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

        public async Task<IActionResult> Edit(int id)
        {
            var project = await _projectRepository.GetByIdAsync(id);

            if (project == null)
            {
                return View("Error");
            }

            var projectStatusSelectList = await _projectStatusRepository.GetSelectItems();
            var teamSelectList = await _teamRepository.GetSelectItems();

            var projectViewModel = new EditProjectViewModel()
            {
                Id = id,
                Name = project.Name,
                CompanyId = project.CompanyId,

                SelectedTeamId = project.TeamId,
                TeamSelectList = teamSelectList.ToList(),

                SelectedProjectStatusId = project.ProjectStatusId,
                ProjectStatusSelectList = projectStatusSelectList.ToList(),
            };

            return View(projectViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, EditProjectViewModel projectViewModel)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "editing Project failed");
                return View(projectViewModel);
            }

            var project = await _projectRepository.GetByIdAsync(id);
            if (project == null)
            {
                return View("Error", projectViewModel);
            }

            project.Name = projectViewModel.Name;
            project.CompanyId = projectViewModel.CompanyId;
            project.ProjectStatusId = projectViewModel.SelectedProjectStatusId;
            project.TeamId = projectViewModel.SelectedTeamId;

            var result = _projectRepository.Update(project);
            if (!result)
            {
                ModelState.AddModelError("", "editing Project failed");
                return View(projectViewModel);
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var currentUserRole = await _userRepository.GetCurrentUserRoleAsync();
            if (currentUserRole != UserRoles.Admin)
            {
                ModelState.AddModelError("", "Action not Allowed");
                return RedirectToAction("Edit", id);
            }

            var project = await _projectRepository.GetByIdAsync(id);
            if (project == null)
            {
                return NotFound();
            }

            _projectRepository.Delete(project);

            return RedirectToAction("Index");
        }
    }
}
