using Microsoft.AspNetCore.Mvc;
using WeldingJobTrackerWebApp.Interfaces;
using WeldingJobTrackerWebApp.Models.ViewModels.ViewDashboard;

namespace WeldingJobTrackerWebApp.Controllers
{
    public class DashboardController : Controller
    {
        private readonly IDashboardRepository _dashboardRepository;
        private readonly IUserRepository _userRepository;

        public DashboardController(IDashboardRepository dashboardRepository, IUserRepository userRepository)
        {
            _dashboardRepository = dashboardRepository;
            _userRepository = userRepository;
        }

        public async Task<IActionResult> Index()
        {
            var projects = await _dashboardRepository.GetAllUserProjects();
            var currentUser = await _userRepository.GetCurrentUserAsync();
            var users = await _userRepository.GetAllAsync();

            var dashBoardViewModel = new DashboardViewModel
            {
                Projects = projects,
                CurrentUser = currentUser,
                Users = users.ToList(),
            };

            return View(dashBoardViewModel);
        }
    }
}
