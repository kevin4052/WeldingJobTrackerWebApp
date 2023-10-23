using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WeldingJobTrackerWebApp.Data;

namespace WeldingJobTrackerWebApp.Controllers
{
    public class ClientController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ClientController(ApplicationDbContext context) 
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var clients = _context.Clients.Include(a => a.Address).ToList();
            return View(clients);
        }

        public IActionResult Detail(int id) 
        {
            var client = _context.Clients.Include(a => a.Address).FirstOrDefault(c => c.Id == id);
            return View(client);
        }
    }
}
