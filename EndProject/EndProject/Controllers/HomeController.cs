using EndProject.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace EndProject.Controllers
{
    public class HomeController : Controller
    {
      

        public IActionResult Index()
        {
            return View();
        }



    }
}