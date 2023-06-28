using EndProject.Areas.Admin.ViewModels.AboutUs;
using EndProject.Areas.Admin.ViewModels.Slider;
using EndProject.Areas.Admin.ViewModels.SpecialCollection;
using EndProject.Helpers;
using EndProject.Models;
using EndProject.Services;
using EndProject.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EndProject.Areas.Admin.Controllers
{
     [Area("Admin")]
    public class AboutUsController : Controller
    {
        private readonly IWebHostEnvironment _env;       
        private readonly ICrudService<AboutUs> _crudService;
        private readonly IAboutUsService _aboutUsService;


        public AboutUsController(IWebHostEnvironment env,
                                 ICrudService<AboutUs> crudService,
                                 IAboutUsService aboutUsService)
        {
            _env = env;
            _aboutUsService = aboutUsService;
            _crudService = crudService;

        }

        public async Task<IActionResult> Index()
        {
            IEnumerable<AboutUs> dbaboutUs = await _aboutUsService.GetAllAsync();
            return View(dbaboutUs);
            
        }


        [HttpGet]
        public async Task<IActionResult> Detail(int? id)
        {

            if (id == null) return BadRequest();
            AboutUs dbAbouUs = await _aboutUsService.GetByIdAsync((int)id);

            if (dbAbouUs is null) return NotFound();

            return View(dbAbouUs);
        }



        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AboutUsCreateVM model)
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


                AboutUs aboutUs = new()
                {
                    Image = model.Photo.CreateFile(_env, "assets/img"),
                    Title = model.Title,
                    Description = model.Description,

                };

                await _crudService.CreateAsync(aboutUs);
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
                AboutUs dbAbouUs = await _aboutUsService.GetByIdAsync((int)id);
                if (dbAbouUs is null) return NotFound();

                AboutUsUpdateVM model = new()
                {
                    Image = dbAbouUs.Image,
                    Title = dbAbouUs.Title,
                    Description = dbAbouUs.Description,

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
        public async Task<IActionResult> Edit(int? id, AboutUsUpdateVM model)
        {
            try
            {
                if (id is null) return BadRequest();
                AboutUs dbAboutUs = await _aboutUsService.GetByIdAsync((int)id);
                if (dbAboutUs is null) return NotFound();

                AboutUsUpdateVM aboutUsUpdateVM = new()
                {
                    Image = dbAboutUs.Image
                };


                if (model.Photo is not null)
                {
                    if (!model.Photo.CheckFileType("image/"))
                    {
                        ModelState.AddModelError("Photo", "File type must be image");
                        return View(aboutUsUpdateVM);
                    }
                    if (!model.Photo.CheckFileSize(200))
                    {
                        ModelState.AddModelError("Photo", "Image size must be max 200kb");
                        return View(aboutUsUpdateVM);
                    }
                    string path = FileHelper.GetFilePath(_env.WebRootPath, "assets/img", aboutUsUpdateVM.Image);
                    FileHelper.DeleteFile(path);

                    dbAboutUs.Image = model.Photo.CreateFile(_env, "assets/img");
                }
                else
                {
                    AboutUs newAboutUs = new()
                    {
                        Image = dbAboutUs.Image
                    };
                }



                dbAboutUs.Title = model.Title;
                dbAboutUs.Description = model.Description;
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
                AboutUs dbAboutUs = await _aboutUsService.GetByIdAsync((int)id);
                if (dbAboutUs is null) return NotFound();

                string path = FileHelper.GetFilePath(_env.WebRootPath, "assets/img", dbAboutUs.Image);
                FileHelper.DeleteFile(path);

                _crudService.Delete(dbAboutUs);
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
