using EndProject.Data;
using EndProject.Helpers.Enums;
using EndProject.Models;
using EndProject.ViewModels.Account;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace EndProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    
    public class AdminLoginController : Controller
    {
        private readonly UserManager<AppUser> _usermanager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly AppDbContext _context;

        public AdminLoginController(UserManager<AppUser> usermanager, SignInManager<AppUser> signInManager, RoleManager<IdentityRole> roleManager, AppDbContext context)
        {
            _usermanager = usermanager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _context = context;
        }

        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginVM account)
        {
            if (!ModelState.IsValid) return View();

            AppUser user = await _usermanager.FindByEmailAsync(account.EmailOrUsername);

            if (user is null)
            {
                user = await _usermanager.FindByNameAsync(account.EmailOrUsername);
            }

            if (user is null)
            {
                ModelState.AddModelError(string.Empty, "Email or password is wrong");
                return View(account);
            }

            var userRoles = await _usermanager.GetRolesAsync(user);

            if (userRoles.Contains(Roles.SuperAdmin.ToString()) || userRoles.Contains(Roles.Admin.ToString()) )
            {
                Microsoft.AspNetCore.Identity.SignInResult result = await _signInManager.PasswordSignInAsync(user, account.Password, account.IsRemember, true);

                if (!result.Succeeded)
                {
                    if (result.IsLockedOut)
                    {
                        ModelState.AddModelError("", "Due to your efforts, our account was blocked for 5 minutes");
                    }
                    ModelState.AddModelError("", "Username or password is incorrect");
                    return View();
                }
            }
            return RedirectToAction("Index", "Dashboard");

        }



        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction(nameof(Login));
        }


    }
}
