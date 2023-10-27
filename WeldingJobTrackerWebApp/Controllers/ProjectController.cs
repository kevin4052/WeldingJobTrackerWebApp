using Microsoft.AspNetCore.Mvc;
using WeldingJobTrackerWebApp.Interfaces;
using WeldingJobTrackerWebApp.Models;
using WeldingJobTrackerWebApp.Repositories;

namespace WeldingJobTrackerWebApp.Controllers
{
    public class ProjectController : Controller
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IProjectStatusRepository _projectStatusRepository;

        public ProjectController(IProjectRepository projectRepository, IProjectStatusRepository projectStatusRepository)
        {
            _projectRepository = projectRepository;
            _projectStatusRepository = projectStatusRepository;
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

        public IActionResult Create() 
        {
            var projecStatuses = _projectStatusRepository.GetAll();
            return View(projecStatuses);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Project project)
        {
            if (!ModelState.IsValid)
            {
                return View(project);
            }

            _projectRepository.Add(project);
            return RedirectToAction("Index");
        }
    }
}
