using EndProject.Data;
using EndProject.Models;
using EndProject.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EndProject.Services
{
    public class HomeAdvertisingService : IHomeAdvertisingService
    {

        private readonly AppDbContext _context;


        public HomeAdvertisingService(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<HomeAddvertising>> GetAllAsync()
        {
            return await _context.HomeAddvertisings.ToListAsync();

        }

        public async Task<HomeAddvertising> GetByIdAsync(int? id)
        {
            return await _context.HomeAddvertisings.FirstOrDefaultAsync(s => s.Id == id);
        }
    }
}
