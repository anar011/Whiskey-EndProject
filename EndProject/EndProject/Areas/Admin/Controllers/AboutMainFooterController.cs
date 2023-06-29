using EndProject.Areas.Admin.ViewModels.AboutMainFooter;
using EndProject.Areas.Admin.ViewModels.AboutUs;
using EndProject.Helpers;
using EndProject.Models;
using EndProject.Services;
using EndProject.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EndProject.Areas.Admin.Controllers
{

    [Area("Admin")]
    public class AboutMainFooterController : Controller
    {
        private readonly IWebHostEnvironment _env;
        private readonly ICrudService<AboutMainFooter> _crudService;
       private readonly IAboutMainFooterService _aboutMainFooterService;


        public AboutMainFooterController(IWebHostEnvironment env,
                                 ICrudService<AboutMainFooter> crudService,
                                 IAboutMainFooterService aboutMainFooterService)
        {
            _aboutMainFooterService = aboutMainFooterService;
            _env = env;
            _crudService = crudService;
        }




        public async Task<IActionResult> Index()
        {
            IEnumerable<AboutMainFooter> dbaboutMainFooter = await _aboutMainFooterService.GetAllAsync();
            return View(dbaboutMainFooter);
        }


        [HttpGet]
        public async Task<IActionResult> Detail(int? id)
        {

            if (id == null) return BadRequest();
            AboutMainFooter dbAboutMainFooter = await _aboutMainFooterService.GetByIdAsync((int)id);

            if (dbAboutMainFooter is null) return NotFound();

            return View(dbAboutMainFooter);
        }


        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AboutMainFooterCreateVM model)
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


                AboutMainFooter aboutMainFooter = new()
                {
                    Image = model.Photo.CreateFile(_env, "assets/img"),
                    Title = model.Title,
                    Description = model.Description,

                };

                await _crudService.CreateAsync(aboutMainFooter);
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

                AboutMainFooter dbAboutMainFooter = await _aboutMainFooterService.GetByIdAsync((int)id);

                if (dbAboutMainFooter is null) return NotFound();

                AboutMainFooterUpdateVM model = new()
                {
                    Image = dbAboutMainFooter.Image,
                    Title = dbAboutMainFooter.Title,
                    Description = dbAboutMainFooter.Description,

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
        public async Task<IActionResult> Edit(int? id, AboutMainFooterUpdateVM model)
        {
            try
            {
                if (id is null) return BadRequest();
                AboutMainFooter dbAboutMainFooter = await _aboutMainFooterService.GetByIdAsync((int)id);
                if (dbAboutMainFooter is null) return NotFound();

                AboutMainFooterUpdateVM aboutMainFooterUpdateVM = new()
                {
                    Image = dbAboutMainFooter.Image
                };


                if (model.Photo is not null)
                {
                    if (!model.Photo.CheckFileType("image/"))
                    {
                        ModelState.AddModelError("Photo", "File type must be image");
                        return View(aboutMainFooterUpdateVM);
                    }
                    if (!model.Photo.CheckFileSize(200))
                    {
                        ModelState.AddModelError("Photo", "Image size must be max 200kb");
                        return View(aboutMainFooterUpdateVM);
                    }
                    string path = FileHelper.GetFilePath(_env.WebRootPath, "assets/img", aboutMainFooterUpdateVM.Image);
                    FileHelper.DeleteFile(path);

                    dbAboutMainFooter.Image = model.Photo.CreateFile(_env, "assets/img");
                }
                else
                {
                    AboutMainFooter newAboutMainFooter = new()
                    {
                        Image = dbAboutMainFooter.Image
                    };
                }



                dbAboutMainFooter.Title = model.Title;
                dbAboutMainFooter.Description = model.Description;
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
                AboutMainFooter dbAboutMainFooter = await _aboutMainFooterService.GetByIdAsync((int)id);
                if (dbAboutMainFooter is null) return NotFound();

                string path = FileHelper.GetFilePath(_env.WebRootPath, "assets/img", dbAboutMainFooter.Image);
                FileHelper.DeleteFile(path);

                _crudService.Delete(dbAboutMainFooter);
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
