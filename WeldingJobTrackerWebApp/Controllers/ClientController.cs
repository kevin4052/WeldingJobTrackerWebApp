using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WeldingJobTrackerWebApp.Data;
using WeldingJobTrackerWebApp.Interfaces;
using WeldingJobTrackerWebApp.Models;
using WeldingJobTrackerWebApp.Repositories;

namespace WeldingJobTrackerWebApp.Controllers
{
    public class ClientController : Controller
    {
        private readonly IClientRepository _clientRepository;

        public ClientController(IClientRepository clientRepository) 
        {
            _clientRepository = clientRepository;
        }
        public async Task<IActionResult> Index()
        {
            var clients = await _clientRepository.GetAll();
            return View(clients);
        }

        public async Task<IActionResult> Detail(int id) 
        {
            var client = await _clientRepository.GetByIdAsync(id);
            return View(client);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Client client)
        {
            if (!ModelState.IsValid)
            {
                return View(client);
            }

            _clientRepository.Add(client);
            return RedirectToAction("Index");
        }
    }
}
