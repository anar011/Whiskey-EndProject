using EndProject.Data;
using EndProject.Helpers;
using EndProject.Models;
using EndProject.Services;
using EndProject.Services.Interfaces;
using EndProject.ViewModels.Blog;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EndProject.Controllers
{
    public class BlogController : Controller
    {
        private readonly IBlogService _blogService;
        private readonly ILayoutService _layoutService;
        private readonly AppDbContext _context;
        public BlogController(AppDbContext context,
                              IBlogService blogService,
                              ILayoutService layoutService)

        {
            _blogService = blogService;
            _layoutService = layoutService;
            _context = context;
        }
        public async Task<IActionResult> Index(int page = 1, int take = 9)
        {
            var blogs = await _blogService.GetAllAsync();
            List<Blog> datas = await _blogService.GetPaginatedDatasAsync(page, take);
            int pageCount = await GetPageCountAsync(take);
            Paginate<Blog> paginatedDatas = new(datas, page, pageCount);
            Blog blog = await _context.Blogs.FirstOrDefaultAsync();

            BlogVM model = new()
            {
                Blogs = blogs.ToList(),
                PaginateDatas = paginatedDatas,
                Blog = blog
 
            };

            return View(model);
        }

        private async Task<int> GetPageCountAsync(int take)
        {
            int prodCount = await _blogService.GetCountAsync();
            return (int)Math.Ceiling((decimal)prodCount / take);
        }



        [HttpGet]
        public async Task<IActionResult> BlogDetail(int? id)
        {
            if (id is null) return BadRequest();
            var dbBlog = await _blogService.GetByIdAsync((int)id);
            if (dbBlog is null) return NotFound();
            var blogs = await _blogService.GetAllAsync();
            BlogInfo bloginfo = await _context.BlogInfos.FirstOrDefaultAsync();
            BlogElement blogElement = await _context.BlogElements.FirstOrDefaultAsync();
            Author author = await _context.Authors.FirstOrDefaultAsync();



            BlogDetailVM model = new()
            {
                Blog = dbBlog,
                Blogs = blogs.ToList(),
                Author = author,
                BlogInfo = bloginfo,
                BlogElement = blogElement,

            };
            return View(model);
        }



    }
}
