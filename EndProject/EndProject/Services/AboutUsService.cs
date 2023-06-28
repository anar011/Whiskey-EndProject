using EndProject.Data;
using EndProject.Models;
using EndProject.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EndProject.Services
{
    public class AboutUsService : IAboutUsService
    {

        private readonly AppDbContext _context;

        public AboutUsService(AppDbContext context)
        {
            _context = context;          
        }


        public async Task<IEnumerable<AboutUs>> GetAllAsync()
        {
            return await _context.AboutUs.ToListAsync();
        }


        public async Task<AboutUs> GetByIdAsync(int? id)
        {
            return await _context.AboutUs.FirstOrDefaultAsync(s => s.Id == id);
        }



    }

}
