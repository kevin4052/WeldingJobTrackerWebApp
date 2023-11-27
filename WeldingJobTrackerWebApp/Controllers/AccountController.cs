using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using WeldingJobTrackerWebApp.Data;
using WeldingJobTrackerWebApp.Models;
using WeldingJobTrackerWebApp.ViewModels;

namespace WeldingJobTrackerWebApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
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
                var company = new List<Company>()
                {
                    new Company()
                    {
                        Name = registerViewModel.CompanyName,
                    }
                };

                newUser.Companies = company;
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
        public IActionResult AddNewUser()
        {
            var newUserViewModel = new NewUserViewModel();
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
                Companies = new List<Company>()
            };

            var newUserResponce = await _userManager.CreateAsync(newUser, newUserViewModel.Password);

            if (!newUserResponce.Succeeded)
            {
                TempData["Error"] = newUserResponce.Errors.First().Description;
                return View(newUserViewModel);
            }

            await _userManager.AddToRoleAsync(newUser, UserRoles.User);

            return RedirectToAction("Index", "Dashboard");
        }
    }
}
