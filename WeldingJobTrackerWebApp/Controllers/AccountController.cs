using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using WeldingJobTrackerWebApp.Data;
using WeldingJobTrackerWebApp.Interfaces;
using WeldingJobTrackerWebApp.Models;
using WeldingJobTrackerWebApp.Models.ViewModels.ViewAccount;

namespace WeldingJobTrackerWebApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IUserRepository _userRepository;
        private readonly IProjectRepository _projectRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly ITeamRepository _teamRepository;
        private readonly IPhotoService _photoService;

        public AccountController(
            UserManager<User> userManager, 
            SignInManager<User> signInManager, 
            RoleManager<IdentityRole> roleManager,
            IUserRepository userRepository, 
            IProjectRepository projectRepository,
            IRoleRepository roleRepository,
            ITeamRepository teamRepository,
            IPhotoService photoService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _userRepository = userRepository;
            _projectRepository = projectRepository;
            _roleRepository = roleRepository;
            _teamRepository = teamRepository;
            _photoService = photoService;
        }

        public IActionResult Login()
        {
            var loginViewModal = new LoginViewModel();
            return View(loginViewModal);
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel) 
        { 
            if (!ModelState.IsValid)
            {
                return View(loginViewModel);
            }

            var user = await _userManager.FindByEmailAsync(loginViewModel.EmailAddress);

            if (user == null) 
            {
                TempData["Error"] = "Email and Password did not match an account";
                return View(loginViewModel);
            }

            var passwordCheck = await _userManager.CheckPasswordAsync(user, loginViewModel.Password);

            if (!passwordCheck)
            {
                TempData["Error"] = "Email and Password did not match an account";
                return View(loginViewModel);
            }

            var result = await _signInManager.PasswordSignInAsync(user, loginViewModel.Password, false, false);

            if (!result.Succeeded)
            { 
                TempData["Error"] = "Email and Password did not match an account";
                return View(loginViewModel);
            }

            return RedirectToAction("Index", "Dashboard");
        }

        [HttpGet]
        public IActionResult Register()
        {
            var registerViewModel = new RegisterViewModel();
            return View(registerViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel registerViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(registerViewModel);
            }

            var user = await _userManager.FindByEmailAsync(registerViewModel.EmailAddress);

            if (user != null) 
            {
                TempData["Error"] = "This email address is already being used";
                return View(registerViewModel);
            }
            
            var newUser = new User
            {
                FirstName = registerViewModel.FirstName,
                LastName = registerViewModel.LastName,
                Email = registerViewModel.EmailAddress,
                UserName = registerViewModel.EmailAddress,
            };

            if (!registerViewModel.CompanyName.IsNullOrEmpty())
            {
                newUser.Company = new Company()
                {
                    Name = registerViewModel.CompanyName,
                };
            }

            var newUserResponce = await _userManager.CreateAsync(newUser, registerViewModel.Password);

            if (!newUserResponce.Succeeded)
            {
                TempData["Error"] = newUserResponce.Errors.First().Description;
                return View(registerViewModel);
            }

            await _userManager.AddToRoleAsync(newUser, UserRoles.Admin);

            return RedirectToAction("Login", "Account");
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public async Task<IActionResult> AddNewUser()
        {
            var currentUser = await _userRepository.GetCurrentUserAsync();
            var roleSelectItems = await _roleRepository.GetAllSelectRoles();

            var newUserViewModel = new NewUserViewModel()
            {
                CompanyId = currentUser.CompanyId,
                RoleSelectList = roleSelectItems,
            };

            return View(newUserViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> AddNewUser(NewUserViewModel newUserViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(newUserViewModel);
            }

            var user = await _userManager.FindByEmailAsync(newUserViewModel.EmailAddress);

            if (user != null)
            {
                TempData["Error"] = "This email address is already being used";
                return View(newUserViewModel);
            }

            var newUser = new User
            {
                FirstName = newUserViewModel.FirstName,
                LastName = newUserViewModel.LastName,
                Email = newUserViewModel.EmailAddress,
                UserName = newUserViewModel.EmailAddress,
                CompanyId = newUserViewModel.CompanyId,
            };

            var newUserResponce = await _userManager.CreateAsync(newUser, newUserViewModel.Password);

            if (!newUserResponce.Succeeded)
            {
                TempData["Error"] = newUserResponce.Errors.First().Description;
                return View(newUserViewModel);
            }

            var selectedRole = _roleManager.Roles.FirstOrDefault(r => r.Id == newUserViewModel.SelectedRoleId);

            await _userManager.AddToRoleAsync(newUser, selectedRole.Name);

            return RedirectToAction("Index", "Dashboard");
        }

        [HttpGet]
        public async Task<IActionResult> Details(string id)
        {
            if (id.IsNullOrEmpty())
            {
                View("index");
            }

            var user = await _userRepository.GetUserbyIdAsync(id);
            var teams = await _teamRepository.GetUserTeams(user.Id);
            var projects = await _projectRepository.GetUserProjects(user.Id);

            var accountViewModal = new DetailAccountViewModal
            {
                Id = id,
                CompanyId = user.Company.Id,
                CompanyName = user.Company.Name,
                Address = user.Address,
                FirstName = user.FirstName,
                MiddleName = user.MiddleName,
                LastName= user.LastName,
                EmailAddress = user.Email,
                ImageUrl = user?.Image?.Url,
                Teams = teams.ToList(),
                Projects = projects.ToList(),
            };

            return View(accountViewModal);
        }

        [HttpPost]
        public async Task<IActionResult> Details(string id, DetailAccountViewModal detailAccountViewModal)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Failed to update user");
                return View(detailAccountViewModal);
            }

            var user = await _userRepository.GetUserbyIdAsync(id);

            if (user == null)
            {
                return View("Error");
            }

            user.Id = id;
            user.FirstName = detailAccountViewModal.FirstName;
            user.MiddleName = detailAccountViewModal.MiddleName;
            user.LastName = detailAccountViewModal.LastName;
            user.Email = detailAccountViewModal.EmailAddress;
            user.UserName = detailAccountViewModal.EmailAddress;
            user.AddressId = user.AddressId ?? 0;

            if (user.AddressId == 0)
            {
                user.Address = new Address
                {
                    Id = (int)user.AddressId,
                    Street1 = detailAccountViewModal.Address.Street1,
                    Street2 = detailAccountViewModal.Address.Street2,
                    City = detailAccountViewModal.Address.City,
                    State = detailAccountViewModal.Address.State,
                    PostalCode = detailAccountViewModal.Address.PostalCode,
                };

            } else
            {
                user.Address.Street1 = detailAccountViewModal.Address.Street1;
                user.Address.Street2 = detailAccountViewModal.Address.Street2;
                user.Address.City = detailAccountViewModal.Address.City;
                user.Address.State = detailAccountViewModal.Address.State;
                user.Address.PostalCode = detailAccountViewModal.Address.PostalCode;
            }

            var imageUploadresult = await _photoService.AddPhotoAsync(detailAccountViewModal.Image);

            if (imageUploadresult != null && imageUploadresult.Error != null)
            {
                var errorMessage = imageUploadresult?.Error?.Message ?? "Error uploading image";
                ModelState.AddModelError("Image", errorMessage);
                return View(detailAccountViewModal);
            }

            if (!string.IsNullOrEmpty(user?.Image?.publicId) && imageUploadresult != null)
            {
                await _photoService.DeletePhotoAsync(user.Image.publicId);
            }
            
            if (imageUploadresult != null)
            {
                user.ImageId = user?.Image?.Id ?? 0;
                user.Image = new Image
                {
                    Id = user?.Image?.Id ?? 0,
                    publicId = imageUploadresult?.PublicId ?? user.Image.publicId,
                    Url = imageUploadresult?.Url.ToString() ?? user.Image.Url
                };
            }

            var result = await _userRepository.Update(user);

            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Failed to update user");
                return View(detailAccountViewModal);
            }

            return View(detailAccountViewModal);
        }
    }
}
