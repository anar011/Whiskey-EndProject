using EndProject.Data;
using EndProject.Models;
using EndProject.Services.Interfaces;
using EndProject.ViewModels.Home;
using EndProject.ViewModels.Product;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Drawing;

namespace EndProject.Controllers
{

    public class HomeController : Controller
    {
        private readonly IHomeAdvertisingService _homeAdvertisingService;
        private readonly ISliderService _sliderService;
        private readonly ISpecialCollectionService _specialCollectionService;
        private readonly AppDbContext _context;
        private readonly  IBlogService _blogService;
        private readonly IProductService _productService;



        public HomeController(AppDbContext context,
                              ISliderService sliderService,
                            ISpecialCollectionService specialCollectionService,
                            IHomeAdvertisingService homeAdvertisingService,
                            IBlogService blogService,
                            IProductService productService)

                                                               
        {
            _sliderService = sliderService;
            _specialCollectionService = specialCollectionService;
            _context = context;
            _homeAdvertisingService = homeAdvertisingService;
            _blogService = blogService;
            _productService = productService;   


        }





        public async Task<IActionResult> Index()
        {
            SpecialCollection specialCollection = await _context.SpecialCollections.FirstOrDefaultAsync();

            HomeVM model = new()
            {
               
                Sliders = await _sliderService.GetAllAsync(),
                SpecialCollection = specialCollection,
                HomeAddvertisings = await _homeAdvertisingService.GetAllAsync(),
                Blogs = await _blogService.GetAllAsync(),
                Products = await _productService.GetAllAsync(),



            };


            return View(model);
        }


        public async Task<IActionResult> GetDataProductModal(int? id)
        {
            try
            {
                if (id is null) return BadRequest();
                var dbProduct = await _productService.GetDatasModalProductByIdAsyc((int)id);
                if (dbProduct is null) return NotFound();
                var price = dbProduct.ProductCapacities.Select(x => x.Price).FirstOrDefault();
                ModalVM model = new()
                {
                    Id = dbProduct.Id,
                    Name = dbProduct.Name,
                    Price = price,
                    Image = dbProduct.Image,
                    Description = dbProduct.Description,
                };

                return Ok(model);
            }
            catch (Exception ex)
            {
                ViewBag.error = ex.Message;
                return View();
            }
        }


    }
}