using EndProject.Data;
using EndProject.Models;
using EndProject.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EndProject.Services
{
    public class BlogService : IBlogService
    {
        private readonly AppDbContext _context;
        public BlogService(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Blog>> GetAllAsync()
        {
            return await _context.Blogs.Include(b => b.BlogInfos).Include(b => b.Author).Include(b => b.BlogElements).ThenInclude(b => b.BlogElementLists).ToListAsync();
        }

        public async Task<Author> GetAuthorById(int? id)
        {
            return await _context.Authors.FindAsync(id);
        }

        public async Task<BlogElement> GetBlogElementById(int? id)
        {
            return await _context.BlogElements.FindAsync(id);
        }

        public async Task<BlogElementList> GetBlogElementListById(int? id)
        {
            return await _context.BlogElementLists.FindAsync(id);
        }

        public async Task<Blog> GetByIdAsync(int? id)
        {
            return await _context.Blogs
                .Include(b => b.BlogInfos)
                .Include(b => b.Author)
                .Include(b => b.BlogElements)
                .ThenInclude(b => b.BlogElementLists)
                .FirstOrDefaultAsync(b => b.Id == id);
        }

        public async Task<BlogInfo> GetInfoById(int? id)
        {
            return await _context.BlogInfos.FindAsync(id);
        }
        public async Task<int> GetCountAsync()
        {
            return await _context.Blogs.CountAsync();
        }


        public async Task<List<Blog>> GetPaginatedDatasAsync(int page = 1, int take = 2)
        {
            return await _context.Blogs
           .Include(b => b.BlogInfos)
           .Include(b => b.Author)
           .Include(b => b.BlogElements)
           .ThenInclude(b => b.BlogElementLists)
           .Skip((page * take) - take)
           .Take(take)
           .ToListAsync();
        }
    }
}
