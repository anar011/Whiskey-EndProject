using EndProject.Areas.Admin.ViewModels.Slider;
using EndProject.Helpers;
using EndProject.Models;
using EndProject.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EndProject.Areas.Admin.Controllers
{

    [Area("Admin")]
    public class SliderController : Controller
    {

        private readonly IWebHostEnvironment _env;
        private readonly ISliderService _sliderService;
        private readonly ICrudService<Slider> _crudService;


        public SliderController(IWebHostEnvironment env,
                                ISliderService sliderService,
                                ICrudService<Slider> crudService)
        {
            _env = env;
            _sliderService = sliderService;
            _crudService = crudService;
        }

        public async Task<IActionResult> Index()
        {
            IEnumerable<Slider> dbSlider = await _sliderService.GetAllAsync();
            return View(dbSlider);
        }




        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SliderCreateVM model)
        {
            try
            {
                if (!ModelState.IsValid) return View();

                if (!model.Photo.CheckFileType("image/"))
                {
                    ModelState.AddModelError("Photo", "File type must be image");
                    return View();
                }
                if (!model.Photo.CheckFileSize(200))
                {
                    ModelState.AddModelError("Photo", "Image size must be max 200kb");
                    return View();
                }
                var convertedPrice = decimal.Parse(model.Price);
                Slider slider = new()
                {
                    Image = model.Photo.CreateFile(_env, "assets/img"),
                    Name = model.Name,
                    Title = model.Title,
                    Description = model.Description,
                    Price = convertedPrice
                };

                await _crudService.CreateAsync(slider);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.error = ex.Message;
                return View();
            }
        }



    }
}
