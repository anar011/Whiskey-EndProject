using EndProject.Models;
using EndProject.Services.Interfaces;
using EndProject.ViewModels.Home;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace EndProject.Controllers
{

    public class HomeController : Controller
    {

        private readonly ISliderService _sliderService;


        public HomeController(ISliderService sliderService)
        {
            _sliderService = sliderService;
        }





        public async Task<IActionResult> Index()
        {
            HomeVM model = new()
            {
               
                Sliders = await _sliderService.GetAllAsync()
               
            };


            return View(model);
        }



    }
}