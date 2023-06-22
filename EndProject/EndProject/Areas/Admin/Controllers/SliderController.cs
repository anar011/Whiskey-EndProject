using EndProject.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EndProject.Areas.Admin.Controllers
{

    [Area("Admin")]
    public class SliderController : Controller
    {

        private readonly IWebHostEnvironment _env;
        private readonly ISliderService _sliderService;
        public IActionResult Index()
        {
            return View();
        }
    }
}
