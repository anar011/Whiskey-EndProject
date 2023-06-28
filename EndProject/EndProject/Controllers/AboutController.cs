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

        public AboutController(AppDbContext context,
            IAboutUsService aboutUsService,
            IAboutAdvertisingService aboutAdvertisingService)
        {
            _aboutUsService = aboutUsService;
            _context = context;
            _aboutAdvertisingService = aboutAdvertisingService;
        }


        public async Task<IActionResult> Index()
        {
            AboutUs aboutUs = await _context.AboutUs.FirstOrDefaultAsync();

            AboutVM model = new()
            {
                AboutUs = aboutUs,
                   AboutAdvertisings = await _aboutAdvertisingService.GetAllAsync(),
            };

            return View(model);
        }


    }
}
