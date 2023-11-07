using Microsoft.AspNetCore.Mvc;
using WeldingJobTrackerWebApp.Interfaces;
using WeldingJobTrackerWebApp.Models;
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
        public async Task<IActionResult> Create(CreateClientViewModel clientViewModel)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "creating client failed");
                return View(clientViewModel);
            }

            var imageUploadresult = await _photoService.AddPhotoAsync(clientViewModel.Image);

            if (imageUploadresult.Error != null)
            {
                ModelState.AddModelError("", "creating client failed");
                return View(clientViewModel);
            }

            var client = new Client
            {
                Name = clientViewModel.Name,
                Address = new Address
                {
                    Street1 = clientViewModel.Address.Street1,
                    Street2 = clientViewModel.Address.Street2,
                    City = clientViewModel.Address.City,
                    State = clientViewModel.Address.State,
                    PostalCode = clientViewModel.Address.PostalCode,
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

            var clientViewModel = new EditClientViewModel
            {
                Id = client.Id,
                Name = client.Name,
                AddressId = client.AddressId,
                Address = client.Address,
                ImageUrl = client?.Image?.Url
            };

            return View(clientViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, EditClientViewModel clientViewModel)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Failed to update client");
                return View(clientViewModel);
            }

            var client = await _clientRepository.GetByIdAsyncNoTracking(id);

            if (client == null)
            {
                return View("Error");
            }

            var imageUploadresult = await _photoService.AddPhotoAsync(clientViewModel.Image);

            if (imageUploadresult.Error != null)
            {
                ModelState.AddModelError("Image", "Photo upload failed");
                return View(clientViewModel);
            }

            client.Name = clientViewModel.Name;
            client.Address = clientViewModel.Address;
            client.Image.Url = clientViewModel.ImageUrl;

            _clientRepository.Update(client);

            return Redirect("index");
        }
    }
}
