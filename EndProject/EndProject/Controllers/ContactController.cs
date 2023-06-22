using Microsoft.AspNetCore.Mvc;

namespace EndProject.Controllers
{
    public class ContactController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
