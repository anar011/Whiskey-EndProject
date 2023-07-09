using EndProject.Areas.Admin.ViewModels.ContactInfo;
using EndProject.Areas.Admin.ViewModels.Slider;
using EndProject.Data;
using EndProject.Helpers;
using EndProject.Models;
using EndProject.Services;
using EndProject.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EndProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ContactInfoController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;
        private readonly IContactInfoService _contactInfoService;
        private readonly ICrudService<ContactInfo> _crudService;


        public ContactInfoController(AppDbContext context,
                                IWebHostEnvironment env,
                                IContactInfoService contactInfoService,
                                ICrudService<ContactInfo> crudService)
        {
            _contactInfoService = contactInfoService;
            _env = env;
            _context = context;
            _crudService = crudService;

        }
        public async Task<IActionResult> Index()
        {

            IEnumerable<ContactInfo> dbContactInfo = await _contactInfoService.GetAllAsync();
            return View(dbContactInfo);

        }




        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ContactInfoCreateVM model)
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

             
                ContactInfo contactInfo = new()
                {
                    Image = model.Photo.CreateFile(_env, "assets/img"),
                    Title = model.Title,
                    Description = model.Description,
                  
                };

                await _crudService.CreateAsync(contactInfo);
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
                ContactInfo dbContactInfo = await _contactInfoService.GetByIdAsync((int)id);
                if (dbContactInfo is null) return NotFound();

                ContactInfoUptadeVM model = new()
                {
                    Image = dbContactInfo.Image,
              
                    Title = dbContactInfo.Title,
                    Description = dbContactInfo.Description,
                  
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
        public async Task<IActionResult> Edit(int? id, ContactInfoUptadeVM model)
        {
            try
            {
                if (id is null) return BadRequest();
                ContactInfo dbContactInfo = await _contactInfoService.GetByIdAsync((int)id);
                if (dbContactInfo is null) return NotFound();

                ContactInfoUptadeVM contactInfoUpdateVM = new()
                {
                   Image  = dbContactInfo.Image
                };


                if (model.Photo is not null)
                {
                    if (!model.Photo.CheckFileType("image/"))
                    {
                        ModelState.AddModelError("Photo", "File type must be image");
                        return View(contactInfoUpdateVM);
                    }
                    if (!model.Photo.CheckFileSize(200))
                    {
                        ModelState.AddModelError("Photo", "Image size must be max 200kb");
                        return View(contactInfoUpdateVM);
                    }
                    string path = FileHelper.GetFilePath(_env.WebRootPath, "assets/img", contactInfoUpdateVM.Image);
                    FileHelper.DeleteFile(path);

                    dbContactInfo.Image = model.Photo.CreateFile(_env, "assets/img");
                }
                else
                {
                    ContactInfo newContactInfo = new()
                    {
                        Image = dbContactInfo.Image
                    };
                }


            
                dbContactInfo.Title = model.Title;
                dbContactInfo.Description = model.Description;
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
                ContactInfo dbContactInfo = await _contactInfoService.GetByIdAsync((int)id);
                if (dbContactInfo is null) return NotFound();

                string path = FileHelper.GetFilePath(_env.WebRootPath, "assets/img", dbContactInfo.Image);
                FileHelper.DeleteFile(path);

                _crudService.Delete(dbContactInfo);
                return RedirectToAction(nameof(Index));
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
            ContactInfo dbContactInfo = await _contactInfoService.GetByIdAsync((int)id);

            if (dbContactInfo is null) return NotFound();

            return View(dbContactInfo);
        }



    }
}
