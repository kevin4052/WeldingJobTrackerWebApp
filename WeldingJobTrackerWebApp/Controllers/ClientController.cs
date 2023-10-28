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
            if (ModelState.IsValid)
            {
                var imageUploadresult = await _photoService.AddPhotoAsync(clientVM.Image);
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
            } else {
                ModelState.AddModelError("", "Photo upload failed");
            }

            return View(clientVM);
        }
    }
}
