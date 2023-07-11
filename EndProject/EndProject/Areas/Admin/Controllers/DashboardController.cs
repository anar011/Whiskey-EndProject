using EndProject.Helpers.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EndProject.Areas.Admin.Controllers
{
    [Authorize(Roles = "SuperAdmin, Admin")]

    [Area("Admin")]
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
