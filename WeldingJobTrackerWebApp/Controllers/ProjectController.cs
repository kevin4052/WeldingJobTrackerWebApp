using Microsoft.AspNetCore.Mvc;

namespace WeldingJobTrackerWebApp.Controllers
{
    public class ProjectController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
