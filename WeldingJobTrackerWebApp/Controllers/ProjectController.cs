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
        private readonly IClientRepository _clientRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ProjectController(
            IProjectRepository projectRepository, 
            IProjectStatusRepository projectStatusRepository, 
            IUserRepository userRepository, 
            IClientRepository clientRepository,
            IHttpContextAccessor httpContextAccessor)
        {
            _projectRepository = projectRepository;
            _projectStatusRepository = projectStatusRepository;
            _userRepository = userRepository;
            _clientRepository = clientRepository;
            _httpContextAccessor = httpContextAccessor;
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
            //var projectStatuses = await _projectStatusRepository.GetAll();
            //var userNameIds = await _userRepository.GetAllUsersNameId();
            //var clients = await _clientRepository.GetAllClientNameId();
            //var currentUserId = _httpContextAccessor.HttpContext?.User?.GetUserId();

            //var projectViewModel = new ProjectViewModel();
            //projectViewModel.CreatedByUserId = currentUserId;

            //projectViewModel.ProjectStatusSelectList = new List<SelectListItem>();
            //foreach (var projectStatus in projectStatuses)
            //{
            //    projectViewModel.ProjectStatusSelectList.Add(new SelectListItem 
            //    { 
            //        Text = projectStatus.Name, 
            //        Value = projectStatus.Code
            //    });
            //}

            //projectViewModel.UserSelectList = new List<SelectListItem>();
            //foreach (var userNameId in userNameIds)
            //{
            //    projectViewModel.UserSelectList.Add(new SelectListItem
            //    {
            //        Text = userNameId.Name,
            //        Value = userNameId.Id
            //    });
            //}

            //projectViewModel.ClientSelectList = new List<SelectListItem>();
            //foreach (var client in clients)
            //{
            //    projectViewModel.ClientSelectList.Add(new SelectListItem
            //    {
            //        Text = client.Name,
            //        Value = client.Id.ToString()
            //    });
            //}

            //return View(projectViewModel);
            throw new NotImplementedException();
        }

        [HttpPost]
        public async Task<IActionResult> Create(ProjectViewModel projectViewModel)
        {
            //if (!ModelState.IsValid)
            //{
            //    ModelState.AddModelError("", "Project save failed");
            //    return View(projectViewModel);                
            //}

            //var selectedProjectStatus = await _projectStatusRepository.GetByCodeAsync(projectViewModel.SelectedProjectStatus);
            //var selectedUser = await _userRepository.GetUserbyIdAsync(projectViewModel.SelectedUser);

            //var project = new Project
            //{
            //    Name = projectViewModel.Name,
            //    ProjectStatusId = selectedProjectStatus.Id,
            //    ClientId = Int32.Parse(projectViewModel?.SelectedClient),
            //    Budget = projectViewModel?.Budget ?? 0,
            //    CostEstimate = projectViewModel?.CostEstimate ?? 0,
            //    Description = projectViewModel?.Description,
            //    Notes = projectViewModel?.Notes,
            //    StartDate = (DateTime)(projectViewModel.StartDate),
            //    EndDate = (DateTime)(projectViewModel.EndDate),
            //    Rate = projectViewModel?.Rate ?? 0,
            //    EstimatedHours = projectViewModel?.EstimatedHours ?? 0,
            //    EstimatedWeldingWire = projectViewModel?.EstimatedWeldingWire ?? 0,
            //    UserMembers = new List<User>(),
            //    CreatedByUserId = projectViewModel.CreatedByUserId,
            //    ManagerUserId = projectViewModel.SelectedManagerId
            //};

            //project.UserMembers.Add(selectedUser);

            //_projectRepository.Add(project);
            return RedirectToAction("Index");
        }
    }
}
