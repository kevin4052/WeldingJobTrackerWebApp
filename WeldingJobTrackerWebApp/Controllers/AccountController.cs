using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WeldingJobTrackerWebApp.Data;
using WeldingJobTrackerWebApp.Models;
using WeldingJobTrackerWebApp.ViewModels;

namespace WeldingJobTrackerWebApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly ApplicationDbContext _context;

        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
        }

        public IActionResult Login()
        {
            return View();
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

            var result = _signInManager.PasswordSignInAsync(user, loginViewModel.Password, false, false);

            if (!result.IsCompletedSuccessfully)
            { 
                TempData["Error"] = "Email and Password did not match an account";
                return View(loginViewModel);
            }


            return RedirectToAction("Index", "Home");
        }

    }
}
