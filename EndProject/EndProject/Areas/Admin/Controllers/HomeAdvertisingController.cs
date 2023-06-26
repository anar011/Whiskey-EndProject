using EndProject.Areas.Admin.ViewModels.HomeAdvertising;
using EndProject.Areas.Admin.ViewModels.Slider;
using EndProject.Helpers;
using EndProject.Models;
using EndProject.Services;
using EndProject.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EndProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomeAdvertisingController : Controller
    {
        private readonly IWebHostEnvironment _env;
        private readonly IHomeAdvertisingService _homeAdvertisingService;
        private readonly ICrudService<HomeAddvertising> _crudService;

        public HomeAdvertisingController(IWebHostEnvironment env,
                                IHomeAdvertisingService homeAdvertisingService,
                                ICrudService<HomeAddvertising> crudService)
        {
            _env = env;
            _homeAdvertisingService = homeAdvertisingService;
            _crudService = crudService;
        }
        public async Task<IActionResult> Index()
        {
            IEnumerable<HomeAddvertising> dbSlider = await _homeAdvertisingService.GetAllAsync();
            return View(dbSlider);
        }

        [HttpGet]
        public async Task<IActionResult> Detail(int? id)
        {

            if (id == null) return BadRequest();
            HomeAddvertising dbHomeAdvertising = await _homeAdvertisingService.GetByIdAsync((int)id);

            if (dbHomeAdvertising is null) return NotFound();

            return View(dbHomeAdvertising);
        }


        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(HomeAdvertisingCreateVM model)
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
               
                HomeAddvertising homeAddvertising    = new()
                {
                    Image = model.Photo.CreateFile(_env, "assets/img"),     
                    Title = model.Title,
                    Description = model.Description,
           
                };

                await _crudService.CreateAsync(homeAddvertising);
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
                HomeAddvertising dbHomeAdvertising = await _homeAdvertisingService.GetByIdAsync((int)id);
                if (dbHomeAdvertising is null) return NotFound();

                HomeAdvertisingUpdateVM model = new()
                {
                    Image = dbHomeAdvertising.Image,
                  
                    Title = dbHomeAdvertising.Title,
                    Description = dbHomeAdvertising.Description,
             
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
        public async Task<IActionResult> Edit(int? id, HomeAdvertisingUpdateVM model)
        {
            try
            {
                if (id is null) return BadRequest();
                HomeAddvertising dbHomeAdvertising = await _homeAdvertisingService.GetByIdAsync((int)id);
                if (dbHomeAdvertising is null) return NotFound();

                HomeAdvertisingUpdateVM homeAdvertisingUpdateVM = new()
                {
                    Image = dbHomeAdvertising.Image
                };


                if (model.Photo is not null)
                {
                    if (!model.Photo.CheckFileType("image/"))
                    {
                        ModelState.AddModelError("Photo", "File type must be image");
                        return View(homeAdvertisingUpdateVM);
                    }
                    if (!model.Photo.CheckFileSize(200))
                    {
                        ModelState.AddModelError("Photo", "Image size must be max 200kb");
                        return View(homeAdvertisingUpdateVM);
                    }
                    string path = FileHelper.GetFilePath(_env.WebRootPath, "assets/img", homeAdvertisingUpdateVM.Image);
                    FileHelper.DeleteFile(path);

                    dbHomeAdvertising.Image = model.Photo.CreateFile(_env, "assets/img");
                }
                else
                {
                    HomeAddvertising newHomeAdvertising = new()
                    {
                        Image = dbHomeAdvertising.Image
                    };
                }


                dbHomeAdvertising.Title = model.Title;
                dbHomeAdvertising.Description = model.Description;
         
                await _crudService.SaveAsync();
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
