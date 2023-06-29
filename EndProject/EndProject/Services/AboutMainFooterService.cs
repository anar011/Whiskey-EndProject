using EndProject.Data;
using EndProject.Models;
using EndProject.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EndProject.Services
{
    public class AboutMainFooterService : IAboutMainFooterService
    {
        private readonly AppDbContext _context;

        public AboutMainFooterService(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<AboutMainFooter>> GetAllAsync()
        {
            return await _context.AboutMainFooters.ToListAsync();
        }

        public async Task<AboutMainFooter> GetByIdAsync(int? id)
        {
            return await _context.AboutMainFooters.FirstOrDefaultAsync(s => s.Id == id);
        }
    }

}
