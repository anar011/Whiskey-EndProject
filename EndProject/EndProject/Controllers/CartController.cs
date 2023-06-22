using Microsoft.AspNetCore.Mvc;

namespace EndProject.Controllers
{
    public class CartController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
