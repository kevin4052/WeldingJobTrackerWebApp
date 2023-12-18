using Microsoft.AspNetCore.Mvc;
using WeldingJobTrackerWebApp.Data;
using WeldingJobTrackerWebApp.Interfaces;
using WeldingJobTrackerWebApp.Models;
using WeldingJobTrackerWebApp.Models.ViewModels.ViewClient;

namespace WeldingJobTrackerWebApp.Controllers
{
    public class ClientController : Controller
    {
        private readonly IClientRepository _clientRepository;
        private readonly IPhotoService _photoService;
        private readonly IUserRepository _userRepository;

        public ClientController(IClientRepository clientRepository, IPhotoService photoService, IUserRepository userRepository) 
        {
            _clientRepository = clientRepository;
            _photoService = photoService;
            _userRepository = userRepository;
        }
        public async Task<IActionResult> Index()
        {
            var clients = await _clientRepository.GetAll();
            return View(clients);
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
                    publicId = imageUploadresult.PublicId
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
                return View("Edit", clientViewModel);
            }

            var client = await _clientRepository.GetByIdAsyncNoTracking(id);

            if (client == null)
            {
                return View("Error");
            }

            var imageUploadresult = await _photoService.AddPhotoAsync(clientViewModel.Image);
                
            if (imageUploadresult != null && imageUploadresult.Error != null)
            {
                var errorMessage = imageUploadresult?.Error?.Message ?? "Error uploading image";
                ModelState.AddModelError("Image", errorMessage);
                return View(clientViewModel);
            }

            if (!string.IsNullOrEmpty(client.Image?.publicId) && imageUploadresult != null)
            {
                await _photoService.DeletePhotoAsync(client.Image.publicId);
            }

            var clientUpdate = new Client
            {
                Id = id,
                Name = clientViewModel.Name,
                AddressId = clientViewModel.AddressId,
                //Address = clientViewModel.Address,
                Address = new Address
                {
                    Id = client.Address.Id,
                    Street1 = clientViewModel.Address.Street1,
                    Street2 = clientViewModel.Address.Street2,
                    City = clientViewModel.Address.City,
                    State = clientViewModel.Address.State,
                    PostalCode = clientViewModel.Address.PostalCode,
                },
                ImageId = client.Image?.Id ?? 0,
                Image = new Image
                {
                    Id = client.Image?.Id ?? 0,
                    publicId = imageUploadresult?.PublicId ?? client.Image.publicId,
                    Url = imageUploadresult?.Url.ToString() ?? client.Image.Url
                },
            };            

            _clientRepository.Update(clientUpdate);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var currentUserRole = await _userRepository.GetCurrentUserRoleAsync();
            if (currentUserRole != UserRoles.Admin)
            {
                ModelState.AddModelError("", "Action not Allowed");
                return RedirectToAction("Edit", id);
            }

            var client = await _clientRepository.GetByIdAsync(id);
            if (client == null)
            {
                return NotFound();
            }

            if (!string.IsNullOrEmpty(client.Image.publicId))
            {
                _ = _photoService.DeletePhotoAsync(client.Image.publicId);
            }

            _clientRepository.Delete(client);

            return RedirectToAction("Index");
        }
    }
}
