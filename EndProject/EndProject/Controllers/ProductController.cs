using Microsoft.AspNetCore.Mvc;

namespace EndProject.Controllers
{
    public class ProductController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
