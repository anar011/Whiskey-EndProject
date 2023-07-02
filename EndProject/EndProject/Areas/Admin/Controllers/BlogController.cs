using EndProject.Models;
using EndProject.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EndProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BlogController : Controller
    {
        private readonly IWebHostEnvironment _env;
        private readonly IBlogService _blogService;
        private readonly ICrudService<Blog> _crudService;


        public BlogController(IBlogService blogService,
                            ICrudService<Blog> crudService,
                            IWebHostEnvironment env)
        {
            _blogService = blogService;
            _crudService = crudService;
            _env = env;
        }
       
       public async Task<IActionResult> Index()
        {
            return View(await _blogService.GetAllAsync());
        }


    }
}
