using EndProject.Areas.Admin.ViewModels.Slider;
using EndProject.Helpers;
using EndProject.Models;
using EndProject.Services;
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

        

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            try
            {
                if (id is null) return BadRequest();
                Slider dbSlider = await _sliderService.GetByIdAsync((int)id);
                if (dbSlider is null) return NotFound();

                SliderUpdateVM model = new()
                {
                    Image = dbSlider.Image,
                    Name = dbSlider.Name,
                    Title = dbSlider.Title,
                    Description = dbSlider.Description,
                    Price = dbSlider.Price            
                };
                return View(model);
            }
            catch (Exception ex)
            {
                ViewBag.error = ex.Message;
                return View();
            }
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, SliderUpdateVM model)
        {
            try
            {
                if (id is null) return BadRequest();
                Slider dbSlider = await _sliderService.GetByIdAsync((int)id);
                if (dbSlider is null) return NotFound();

                SliderUpdateVM sliderUpdateVM = new()
                {
                    Image = dbSlider.Image
                };


                if (model.Photo is not null)
                {
                    if (!model.Photo.CheckFileType("image/"))
                    {
                        ModelState.AddModelError("Photo", "File type must be image");
                        return View(sliderUpdateVM);
                    }
                    if (!model.Photo.CheckFileSize(200))
                    {
                        ModelState.AddModelError("Photo", "Image size must be max 200kb");
                        return View(sliderUpdateVM);
                    }
                    string path = FileHelper.GetFilePath(_env.WebRootPath, "assets/img", sliderUpdateVM.Image);
                    FileHelper.DeleteFile(path);

                    dbSlider.Image = model.Photo.CreateFile(_env, "assets/img");
                }
                else
                {
                    Slider newSlider = new()
                    {
                        Image = dbSlider.Image
                    };
                }


                dbSlider.Name = model.Name;
                dbSlider.Title = model.Title;
                dbSlider.Description = model.Description;
                dbSlider.Price = model.Price;
                await _crudService.SaveAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.error = ex.Message;
                return View();
            }
        }




        [HttpPost]
        public async Task<IActionResult> Delete(int? id)
        {
            try
            {
                if (id is null) return BadRequest();
                Slider dbSlider = await _sliderService.GetByIdAsync((int)id);
                if (dbSlider is null) return NotFound();

                string path = FileHelper.GetFilePath(_env.WebRootPath, "assets/img", dbSlider.Image);
                FileHelper.DeleteFile(path);

                _crudService.Delete(dbSlider);
                return Ok();
            }
            catch (Exception ex)
            {
                ViewBag.error = ex.Message;
                return View();
            }
        }






        [HttpGet]
        public async Task<IActionResult> Detail(int? id)
        {

            if (id == null) return BadRequest();
            Slider dbSlider = await _sliderService.GetByIdAsync((int)id);

            if (dbSlider is null) return NotFound();

            return View(dbSlider);
        }


    }

}
