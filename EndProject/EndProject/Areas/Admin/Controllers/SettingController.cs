using EndProject.Helpers;
using EndProject.Models;
using EndProject.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EndProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SettingController : Controller
    {

        private readonly IWebHostEnvironment _env;
        private readonly ICrudService<Slider> _crudService;
        private readonly ILayoutService _layoutService;


        public SettingController(IWebHostEnvironment env,
                             ILayoutService layoutService,
                             ICrudService<Slider> crudService)
        {
            _crudService = crudService;
            _env = env;
            _layoutService = layoutService;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _layoutService.GetSettingDatas());
        }


        [HttpGet]
        public IActionResult Edit(int? id)
        {
            var dbSsetting = _layoutService.GetById((int)id);

            return View(dbSsetting);
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, Setting setting)
        {
            try
            {
                var dbSetting = await _layoutService.GetSectionBackgroundImageByIdAsync((int)id);

                if (dbSetting == null) return View();

                if (setting.Photo is not null)
                {
                    if (!setting.Photo.CheckFileType("image/"))
                    {
                        ModelState.AddModelError("Photo", "File type must be image");
                        return View();
                    }
                    if (!setting.Photo.CheckFileSize(200))
                    {
                        ModelState.AddModelError("Photo", "Image size must be max 200kb");
                        return View();
                    }
                    string path = FileHelper.GetFilePath(_env.WebRootPath, "assets/img", dbSetting.Value);
                    FileHelper.DeleteFile(path);

                    dbSetting.Value = setting.Photo.CreateFile(_env, "assets/img");
                }
                else
                {
                    Setting newSectionBg = new()
                    {
                        Value = dbSetting.Value
                    };
                }
                await _crudService.SaveAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {

                throw;
            }
        }



    }
}
