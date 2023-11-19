using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WeldingJobTrackerWebApp.Interfaces;
using WeldingJobTrackerWebApp.Models;
using WeldingJobTrackerWebApp.ViewModels;

namespace WeldingJobTrackerWebApp.Controllers
{
    public class ProjectController : Controller
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IProjectStatusRepository _projectStatusRepository;
        private readonly IUserRepository _userRepository;

        public ProjectController(IProjectRepository projectRepository, IProjectStatusRepository projectStatusRepository, IUserRepository userRepository)
        {
            _projectRepository = projectRepository;
            _projectStatusRepository = projectStatusRepository;
            _userRepository = userRepository;
        }
        public async Task<IActionResult> Index()
        {
            var clients = await _projectRepository.GetAll();
            return View(clients);
        }

        public async Task<IActionResult> Detail(int id)
        {
            var client = await _projectRepository.GetByIdAsync(id);
            return View(client);
        }

        public async Task<IActionResult> Create() 
        {
            var projectStatuses = await _projectStatusRepository.GetAll();
            var userNameIds = await _userRepository.GetAllUsersNameId();

            var viewModel = new ProjectViewModel();
            viewModel.ProjectStatusSelectList = new List<SelectListItem>();

            foreach (var projectStatus in projectStatuses)
            {
                viewModel.ProjectStatusSelectList.Add(new SelectListItem { 
                    Text = projectStatus.Name, 
                    Value = projectStatus.Code
                });
            }

            viewModel.UserSelectList = new List<SelectListItem>();

            foreach (var userNameId in userNameIds)
            {
                viewModel.UserSelectList.Add(new SelectListItem
                {
                    Text = userNameId.Name,
                    Value = userNameId.Id
                });
            }
            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(ProjectViewModel projectVM)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Project save failed");
                return View(projectVM);                
            }

            var selectedProjectStatus = await _projectStatusRepository.GetByCodeAsync(projectVM.SelectedProjectStatus);

            var project = new Project
            {
                Name = projectVM.Name,
                ProjectStatusId = selectedProjectStatus.Id,
                ClientId = projectVM?.Client?.Id ?? 0,
                Budget = projectVM?.Budget ?? 0,
                CostEstimate = projectVM?.CostEstimate ?? 0,
                Description = projectVM?.Description,
                Notes = projectVM?.Notes,
                StartDate = (DateTime)(projectVM.StartDate),
                EndDate = (DateTime)(projectVM.EndDate),
                Rate = projectVM?.Rate ?? 0,
                EstimatedHours = projectVM?.EstimatedHours ?? 0,
                EstimatedWeldingWire = projectVM?.EstimatedWeldingWire ?? 0,
                //Add UserMembers
            };

            _projectRepository.Add(project);
            return RedirectToAction("Index");
        }
    }
}
