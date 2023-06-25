using EndProject.Data;
using EndProject.Models;
using EndProject.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EndProject.Areas.Admin.Controllers
{
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




    }
}
