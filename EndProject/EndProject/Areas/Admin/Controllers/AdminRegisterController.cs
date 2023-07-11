using EndProject.Data;
using EndProject.Helpers.Enums;
using EndProject.Models;
using EndProject.ViewModels.Account;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace EndProject.Areas.Admin.Controllers
{
    [Authorize(Roles = "SuperAdmin")]
    [Area("Admin")]
    public class AdminRegisterController : Controller
    {
        private readonly UserManager<AppUser> _usermanager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly AppDbContext _context;

        public AdminRegisterController(UserManager<AppUser> usermanager, SignInManager<AppUser> signInManager, RoleManager<IdentityRole> roleManager, AppDbContext context)
        {
            _usermanager = usermanager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _context = context;
        }

        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterVM account)
        {
            TempData["Register"] = false;
            if (!ModelState.IsValid) return View();
            AppUser user = new()
            {
                FirstName = account.FirstName,
                LastName = account.LastName,
                Email = account.Email,
                UserName = string.Join("-", account.FirstName, account.LastName),
                EmailConfirmed = true

            };
            IdentityResult result = await _usermanager.CreateAsync(user, account.Password);
            if (!result.Succeeded)
            {
                foreach (IdentityError message in result.Errors)
                {
                    ModelState.AddModelError("", message.Description);
                }
                return View();
            }
            TempData["Register"] = true;
            await _usermanager.AddToRoleAsync(user, Roles.Admin.ToString());

            return RedirectToAction("login", "adminLogin");

        }




    }
}
