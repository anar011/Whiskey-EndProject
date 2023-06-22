using Microsoft.AspNetCore.Mvc;

namespace EndProject.Controllers
{
    public class AboutController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
