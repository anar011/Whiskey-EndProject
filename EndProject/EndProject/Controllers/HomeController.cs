using EndProject.Data;
using EndProject.Models;
using EndProject.Services.Interfaces;
using EndProject.ViewModels.Home;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace EndProject.Controllers
{

    public class HomeController : Controller
    {

        private readonly ISliderService _sliderService;
        private readonly ISpecialCollectionService _specialCollectionService;
        private readonly AppDbContext _context;



        public HomeController(AppDbContext context,
                              ISliderService sliderService,
                            ISpecialCollectionService specialCollectionService
                                                               )
        {
            _sliderService = sliderService;
            _specialCollectionService = specialCollectionService;
            _context = context;
         
        }





        public async Task<IActionResult> Index()
        {
            SpecialCollection specialCollection = await _context.SpecialCollections.FirstOrDefaultAsync();

            HomeVM model = new()
            {
               
                Sliders = await _sliderService.GetAllAsync(),
                SpecialCollection = specialCollection


            };


            return View(model);
        }



    }
}