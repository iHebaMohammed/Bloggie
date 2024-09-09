using Demo.BLL.Interfaces;
using Demo.Pl.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Demo.Pl.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminUsersController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly UserManager<IdentityUser> _userManager;

        public AdminUsersController(
            IUserRepository userRepository,
            UserManager<IdentityUser> userManager
            )
        {
            _userRepository = userRepository;
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var users = await _userRepository.GetAll();
            var usersViewModel = new UserViewModel();
            usersViewModel.Users = new List<User>();
            foreach (var user in users)
            {
                usersViewModel.Users.Add(new User {
                    Id = Guid.Parse(user.Id),
                    Email = user.Email,
                    Username = user.UserName
                });
            }
            return View(usersViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(UserViewModel userViewModel)
        {
            var user = new IdentityUser() 
            {
                UserName = userViewModel.Username,
                NormalizedUserName = userViewModel.Username.ToUpper(),
                Email = userViewModel.Email,
                NormalizedEmail = userViewModel.Email,
            };

            var result = await _userManager.CreateAsync(user , userViewModel.Password);
            if (result.Succeeded) 
            {
                var roles = new List<string>() { "User" };
                if (userViewModel.AdminRoleCheckbox)
                    roles.Add("Admin");
                var roleAssignResult = await _userManager.AddToRolesAsync(user, roles);
                if (roleAssignResult.Succeeded) 
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return RedirectToAction(nameof(Index) , userViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Guid Id)
        {
            var user = await _userManager.FindByIdAsync(Id.ToString());
            if (user != null)
            {
                var result = await _userManager.DeleteAsync(user);
                if (result.Succeeded)
                    return RedirectToAction(nameof(Index));
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
