using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WeldingJobTrackerWebApp.Data;
using WeldingJobTrackerWebApp.Interfaces;
using WeldingJobTrackerWebApp.Models;
using WeldingJobTrackerWebApp.Repositories;
using WeldingJobTrackerWebApp.ViewModels;

namespace WeldingJobTrackerWebApp.Controllers
{
    public class ClientController : Controller
    {
        private readonly IClientRepository _clientRepository;
        private readonly IPhotoService _photoService;

        public ClientController(IClientRepository clientRepository, IPhotoService photoService) 
        {
            _clientRepository = clientRepository;
            _photoService = photoService;
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
        public async Task<IActionResult> Create(CreateClientViewModel clientVM)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "creating client failed");
                return View(clientVM);
            }

            var imageUploadresult = await _photoService.AddPhotoAsync(clientVM.Image);

            if (imageUploadresult.Error != null)
            {
                ModelState.AddModelError("", "creating client failed");
                return View(clientVM);
            }

            var client = new Client
            {
                Name = clientVM.Name,
                Address = new Address
                {
                    Street1 = clientVM.Address.Street1,
                    Street2 = clientVM.Address.Street2,
                    City = clientVM.Address.City,
                    State = clientVM.Address.State,
                    PostalCode = clientVM.Address.PostalCode,
                },
                Image = new Image
                {
                    Url = imageUploadresult.Url.ToString(),
                }

            };

            _clientRepository.Add(client);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(int id)
        {
            var client = await _clientRepository.GetByIdAsync(id);
            if (client == null)
            {
                return View("Error");
            }

            var clientVM = new EditClientViewModel
            {
                Name = client.Name,
                AddressId = client.AddressId,
                Address = client.Address,
                Image = (IFormFile)(client?.Image)
            };

            return View(clientVM);
        }
    }
}
