using Microsoft.AspNetCore.Mvc;
using WeldingJobTrackerWebApp.Interfaces;
using WeldingJobTrackerWebApp.Repositories;

namespace WeldingJobTrackerWebApp.Controllers
{
    public class ProjectController : Controller
    {
        private readonly IProjectRepository _projectRepository;

        public ProjectController(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
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
    }
}
