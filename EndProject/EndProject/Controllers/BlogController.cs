using EndProject.Helpers;
using EndProject.Models;
using EndProject.Services.Interfaces;
using EndProject.ViewModels.Blog;
using Microsoft.AspNetCore.Mvc;

namespace EndProject.Controllers
{
    public class BlogController : Controller
    {
        private readonly IBlogService _blogService;
        private readonly ILayoutService _layoutService;

        public BlogController(IBlogService blogService,
                              ILayoutService layoutService)

        {
            _blogService = blogService;
            _layoutService = layoutService;
        }
        public async Task<IActionResult> Index(int page = 1, int take = 2)
        {
            var blogs = await _blogService.GetAllAsync();
            List<Blog> datas = await _blogService.GetPaginatedDatasAsync(page, take);
            int pageCount = await GetPageCountAsync(take);
            Paginate<Blog> paginatedDatas = new(datas, page, pageCount);

            BlogVM model = new()
            {
                Blogs = blogs.ToList(),
                PaginateDatas = paginatedDatas
 
            };

            return View(model);
        }

        private async Task<int> GetPageCountAsync(int take)
        {
            int prodCount = await _blogService.GetCountAsync();
            return (int)Math.Ceiling((decimal)prodCount / take);
        }

        [HttpGet]
        public async Task<IActionResult> SingleBlog(int? id)
        {
            if (id is null) return BadRequest();
            var dbBlog = await _blogService.GetByIdAsync((int)id);
            if (dbBlog is null) return NotFound();
            var blogs = await _blogService.GetAllAsync();

            BlogVM model = new()
            {
                Blog = dbBlog,
                Blogs = blogs.ToList(),
   
            };
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> BlogDetail(int? id)
        {
            if (id is null) return BadRequest();
            var dbBlog = await _blogService.GetByIdAsync((int)id);
            if (dbBlog is null) return NotFound();
            var blogs = await _blogService.GetAllAsync();

            BlogDetailVM model = new()
            {
                Blog = dbBlog,
                Blogs = blogs.ToList(),
      
            };
            return View(model);
        }



    }
}
