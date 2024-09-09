using Demo.Pl.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Demo.Pl.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public AccountController(
            UserManager<IdentityUser> userManager ,
            SignInManager<IdentityUser> signInManager
            )
        {
            _userManager=userManager;
            _signInManager=signInManager;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel registerViewModel)
        {
            if (ModelState.IsValid)
            {
                var user = new IdentityUser
                {
                    UserName = registerViewModel.UserName,
                    Email = registerViewModel.Email,
                };
                var result = await _userManager.CreateAsync(user, registerViewModel.Password);
                if (result.Succeeded)
                {
                    var roleIdentityResult = await _userManager.AddToRoleAsync(user, "User");
                    if (roleIdentityResult.Succeeded)
                        return RedirectToAction(nameof(Login));
                    else
                        foreach (var error in roleIdentityResult.Errors)
                        {
                            ModelState.AddModelError(string.Empty, error.Description);
                        }
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            return View(registerViewModel);
        }


        [HttpGet]
        public IActionResult Login(string returnUrl)
        {
            var model = new LoginViewModel
            {
                returnUrl = returnUrl,
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel) 
        {
            if (ModelState.IsValid) 
            {
                var signInResult = await _signInManager.PasswordSignInAsync(
                    loginViewModel.UserName,
                    loginViewModel.Password,
                    false, 
                    false
                    );
                if (signInResult != null && signInResult.Succeeded)
                {
                    if (!string.IsNullOrWhiteSpace(loginViewModel.returnUrl))
                    {
                        return Redirect(loginViewModel.returnUrl);
                    }
                    return RedirectToAction("Index", "Home");

                }

            }
            return View(loginViewModel);
        }
        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index" , "Home");
        }

        [HttpGet]
        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
