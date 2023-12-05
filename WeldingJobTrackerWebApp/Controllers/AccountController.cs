using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IRoleRepository _roleRepository;

        public AccountController(
            UserManager<User> userManager, 
            SignInManager<User> signInManager, 
            RoleManager<IdentityRole> roleManager,
            IHttpContextAccessor httpContextAccessor, 
            IRoleRepository roleRepository)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _httpContextAccessor = httpContextAccessor;
            _roleRepository = roleRepository;
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
            var currentUserId = _httpContextAccessor.HttpContext?.User?.GetUserId();
            var currentUser = await _userManager.FindByIdAsync(currentUserId);
            var roles = await _roleRepository.GetAllSelectRoles();

            var newUserViewModel = new NewUserViewModel();
            newUserViewModel.CompanyId = currentUser.CompanyId;

            newUserViewModel.RoleSelectList = new List<SelectListItem>();
            foreach (var role in roles)
            {
                newUserViewModel.RoleSelectList.Add(new SelectListItem
                {
                    Text = role.Name,
                    Value = role.Id.ToString()
                });
            }

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
    }
}
