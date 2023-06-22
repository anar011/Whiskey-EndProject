using Microsoft.AspNetCore.Mvc;

namespace EndProject.Data
{
    public class AppDbContext : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
