﻿using EndProject.Data;
using EndProject.Helpers.Enums;
using EndProject.Models;
using EndProject.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EndProject.Areas.Admin.Controllers
{
    [Authorize(Roles = "SuperAdmin, Admin")]

    [Area("Admin")]

    public class SectionHeaderController : Controller
    {
        private readonly IWebHostEnvironment _env;
        private readonly ICrudService<SectionHeader> _crudService;
        private readonly ILayoutService _layoutService;
        private readonly AppDbContext _context;
        public SectionHeaderController(IWebHostEnvironment env,
                                ILayoutService layoutService, AppDbContext context,
                                ICrudService<SectionHeader> crudService)
        {
            _crudService = crudService;
            _context = context;
            _env = env;
            _layoutService = layoutService;
        }


        public async Task<IActionResult> Index()
        {
            return View(await _layoutService.GetSectionsDatasAsync());
        }



        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            return View(await _layoutService.GetSectionAsync((int)id));
        }


         [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditAsync(int? id, Setting setting)
        {
            try
            {
                var dbSectionHeader =await _layoutService.GetSectionAsync((int)id);

                if (dbSectionHeader == null) return View();

                if (dbSectionHeader.Value.Trim().ToLower() == setting.Value.Trim().ToLower())
                {
                    return RedirectToAction(nameof(Index));
                }

                dbSectionHeader.Value = setting.Value;

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
