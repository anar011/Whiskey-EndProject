using EndProject.Areas.Admin.ViewModels.AboutAdvertising;
using EndProject.Areas.Admin.ViewModels.Slider;
using EndProject.Helpers;
using EndProject.Helpers.Enums;
using EndProject.Models;
using EndProject.Services;
using EndProject.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EndProject.Areas.Admin.Controllers
{
    [Authorize(Roles = "SuperAdmin, Admin")]
    [Area("Admin")]
    public class AboutAdvertisingController : Controller
    {
       

        private readonly IWebHostEnvironment _env;
        private readonly IAboutAdvertisingService _aboutAdvertisingService;
        private readonly ICrudService<AboutAdvertising> _crudService;


        public AboutAdvertisingController(IWebHostEnvironment env,
                                IAboutAdvertisingService aboutAdvertisingService,
                                ICrudService<AboutAdvertising> crudService)

        {
            _env = env;
            _aboutAdvertisingService = aboutAdvertisingService;
            _crudService = crudService;
        }

        public async Task<IActionResult> Index()
        {
            IEnumerable<AboutAdvertising> dbAboutAdvertising = await _aboutAdvertisingService.GetAllAsync();
            return View(dbAboutAdvertising);
        }


        [HttpGet]
        public async Task<IActionResult> Detail(int? id)
        {

            if (id == null) return BadRequest();
            AboutAdvertising dbAboutAdvertising = await _aboutAdvertisingService.GetByIdAsync((int)id);

            if (dbAboutAdvertising is null) return NotFound();

            return View(dbAboutAdvertising);
        }



        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }






        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AboutAdvertisingCreateVM model)
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
               
                AboutAdvertising aboutAdvertising = new()
                {
                    Image = model.Photo.CreateFile(_env, "assets/img"),
                    Number = model.Number,
                    Title = model.Title,
                    Description = model.Description
                  
                };

                await _crudService.CreateAsync(aboutAdvertising);
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
                AboutAdvertising dbAboutAdvertising = await _aboutAdvertisingService.GetByIdAsync((int)id);
                if (dbAboutAdvertising is null) return NotFound();

                AboutAdvertisingUpdateVM model = new()
                {
                    Image = dbAboutAdvertising.Image,
                    Number = dbAboutAdvertising.Number,
                    Title = dbAboutAdvertising.Title,
                    Description = dbAboutAdvertising.Description
            
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
        public async Task<IActionResult> Edit(int? id, AboutAdvertisingUpdateVM model)
        {
            try
            {
                if (id is null) return BadRequest();
                AboutAdvertising dbAboutAdvertising = await _aboutAdvertisingService.GetByIdAsync((int)id);
                if (dbAboutAdvertising is null) return NotFound();

                AboutAdvertisingUpdateVM aboutAdvertisingUpdateVM = new()
                {
                    Image = dbAboutAdvertising.Image
                };


                if (model.Photo is not null)
                {
                    if (!model.Photo.CheckFileType("image/"))
                    {
                        ModelState.AddModelError("Photo", "File type must be image");
                        return View(aboutAdvertisingUpdateVM);
                    }
                    if (!model.Photo.CheckFileSize(200))
                    {
                        ModelState.AddModelError("Photo", "Image size must be max 200kb");
                        return View(aboutAdvertisingUpdateVM);
                    }
                    string path = FileHelper.GetFilePath(_env.WebRootPath, "assets/img", aboutAdvertisingUpdateVM.Image);
                    FileHelper.DeleteFile(path);

                    dbAboutAdvertising.Image = model.Photo.CreateFile(_env, "assets/img");
                }
                else
                {
                    AboutAdvertising newAboutAdvertising = new()
                    {
                        Image = dbAboutAdvertising.Image
                    };
                }


                dbAboutAdvertising.Number = model.Number;
                dbAboutAdvertising.Title = model.Title;
                dbAboutAdvertising.Description = model.Description;
        
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
                AboutAdvertising dbAboutAdvertising = await _aboutAdvertisingService.GetByIdAsync((int)id);
                if (dbAboutAdvertising is null) return NotFound();

                string path = FileHelper.GetFilePath(_env.WebRootPath, "assets/img", dbAboutAdvertising.Image);
                FileHelper.DeleteFile(path);

                _crudService.Delete(dbAboutAdvertising);
                return Ok();
            }
            catch (Exception ex)
            {
                ViewBag.error = ex.Message;
                return View();
            }
        }

    }
}
