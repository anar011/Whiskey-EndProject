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
    public class SpecialCollectionController : Controller
    {
        private readonly IWebHostEnvironment _env;
        private readonly ISpecialCollectionService _specialCollectionService;
        private readonly ICrudService<SpecialCollection> _crudService;


        public SpecialCollectionController(IWebHostEnvironment env,
                                ISpecialCollectionService specialCollectionService,
                                ICrudService<SpecialCollection> crudService)
        {
            _env = env;
            _specialCollectionService = specialCollectionService;
            _crudService = crudService;
        }





        public async Task< IActionResult> Index()
        {
            IEnumerable<SpecialCollection> dbspecialCollections = await _specialCollectionService.GetAllAsync();
            return View(dbspecialCollections);
        }




        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SpecialCollectionCreateVM model)
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
             

                SpecialCollection specialCollection = new()
                {
                    Image = model.Photo.CreateFile(_env, "assets/img"),
                    Title = model.Title,
                    Description = model.Description,
                   
                };

                await _crudService.CreateAsync(specialCollection);
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
            SpecialCollection dbSpecialCollection = await _specialCollectionService.GetByIdAsync((int)id);

            if (dbSpecialCollection is null) return NotFound();

            return View(dbSpecialCollection);
        }



        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            try
            {
                if (id is null) return BadRequest();
                SpecialCollection dbSpecialCollection = await _specialCollectionService.GetByIdAsync((int)id);
                if (dbSpecialCollection is null) return NotFound();

                SliderUpdateVM model = new()
                {
                    Image = dbSpecialCollection.Image,                 
                    Title = dbSpecialCollection.Title,
                    Description = dbSpecialCollection.Description,
                 
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
        public async Task<IActionResult> Edit(int? id, SpecialCollectionUpdateVM model)
        {
            try
            {
                if (id is null) return BadRequest();
                SpecialCollection dbSpecialCollection = await _specialCollectionService.GetByIdAsync((int)id);
                if (dbSpecialCollection is null) return NotFound();

                SpecialCollectionUpdateVM specialCollectionUpdateVM = new()
                {
                    Image = dbSpecialCollection.Image
                };


                if (model.Photo is not null)
                {
                    if (!model.Photo.CheckFileType("image/"))
                    {
                        ModelState.AddModelError("Photo", "File type must be image");
                        return View(specialCollectionUpdateVM);
                    }
                    if (!model.Photo.CheckFileSize(200))
                    {
                        ModelState.AddModelError("Photo", "Image size must be max 200kb");
                        return View(specialCollectionUpdateVM);
                    }
                    string path = FileHelper.GetFilePath(_env.WebRootPath, "assets/img", specialCollectionUpdateVM.Image);
                    FileHelper.DeleteFile(path);

                    dbSpecialCollection.Image = model.Photo.CreateFile(_env, "assets/img");
                }
                else
                {
                    SpecialCollection newSpecialCollection = new()
                    {
                        Image = dbSpecialCollection.Image
                    };
                }



                dbSpecialCollection.Title = model.Title;
                dbSpecialCollection.Description = model.Description;
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
                SpecialCollection dbspecialCollection = await _specialCollectionService.GetByIdAsync((int)id);
                if (dbspecialCollection is null) return NotFound();

                string path = FileHelper.GetFilePath(_env.WebRootPath, "assets/img", dbspecialCollection.Image);
                FileHelper.DeleteFile(path);

                _crudService.Delete(dbspecialCollection);
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
