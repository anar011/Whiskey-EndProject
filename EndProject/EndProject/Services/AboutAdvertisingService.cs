using EndProject.Data;
using EndProject.Models;
using EndProject.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EndProject.Services
{
    public class AboutAdvertisingService : IAboutAdvertisingService
    {
        private readonly AppDbContext _context;


        public AboutAdvertisingService(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<AboutAdvertising>> GetAllAsync()
        {
            return await _context.AboutAdvertisings.ToListAsync();
        }

        public async Task<AboutAdvertising> GetByIdAsync(int? id)
        {
            return await _context.AboutAdvertisings.FirstOrDefaultAsync(s => s.Id == id);
        }
    }
}
