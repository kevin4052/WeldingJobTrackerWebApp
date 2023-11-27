using Microsoft.AspNetCore.Mvc;
using WeldingJobTrackerWebApp.Interfaces;
using WeldingJobTrackerWebApp.ViewModels;

namespace WeldingJobTrackerWebApp.Controllers
{
    public class DashboardController : Controller
    {
        private readonly IDashboardRepository _dashboardRepository;

        public DashboardController(IDashboardRepository dashboardRepository)
        {
            _dashboardRepository = dashboardRepository;
        }

        public async Task<IActionResult> Index()
        {
            //var projects = await _dashboardRepository.GetAllUserProjects();
            var currentUser = await _dashboardRepository.GetCurrentUserAsync();

            var dashBoardViewModel = new DashboardViewModel
            {
                //Projects = projects,
                CurrentUser = currentUser
            };

            return View(dashBoardViewModel);
        }
    }
}
