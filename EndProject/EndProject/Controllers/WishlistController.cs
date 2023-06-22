using Microsoft.AspNetCore.Mvc;

namespace EndProject.Controllers
{
    public class WishlistController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
