using EndProject.Data;
using EndProject.Models;
using EndProject.Services.Interfaces;
using EndProject.ViewModels.About;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EndProject.Controllers
{
    public class AboutController : Controller
    {
        private readonly IAboutUsService _aboutUsService;
        private readonly AppDbContext _context;
        private readonly IAboutAdvertisingService _aboutAdvertisingService;
        private readonly IAboutMainFooterService _aboutMainFooterService;

        public AboutController(AppDbContext context,
            IAboutUsService aboutUsService,
            IAboutAdvertisingService aboutAdvertisingService,
            IAboutMainFooterService aboutMainFooterService)
        {
            _aboutUsService = aboutUsService;
            _context = context;
            _aboutAdvertisingService = aboutAdvertisingService;
            _aboutMainFooterService = aboutMainFooterService;

        }


        public async Task<IActionResult> Index()
        {
            AboutUs aboutUs = await _context.AboutUs.FirstOrDefaultAsync();
            AboutMainFooter aboutMainFooter = await _context.AboutMainFooters.FirstOrDefaultAsync();

            AboutVM model = new()
            {
                AboutUs = aboutUs,
                AboutAdvertisings = await _aboutAdvertisingService.GetAllAsync(),
                AboutMainFooter = aboutMainFooter,
            };

            return View(model);
        }


    }
}
