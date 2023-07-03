using EndProject.Data;
using EndProject.Models;
using EndProject.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EndProject.Services
{
    public class CapacityService : ICapacityService
    {
        private readonly AppDbContext _context;
        public CapacityService(AppDbContext context)
        {
            _context = context;
        }
        public bool CheckByName(string name)
        {
            return _context.Capacities.Any(c => c.Name.Trim().ToLower() == name.Trim().ToLower());
        }

        public async Task<IEnumerable<Capacity>> GetAllAsync()
        {
            return await _context.Capacities.ToListAsync();
        }

        public async Task<Capacity> GetByIdAsync(int? id)
        {
            return await _context.Capacities.AsNoTracking().FirstOrDefaultAsync(c => c.Id == id);
        }
    }
}
