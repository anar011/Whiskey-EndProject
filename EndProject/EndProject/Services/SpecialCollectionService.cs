using EndProject.Data;
using EndProject.Models;
using EndProject.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EndProject.Services
{
    public class SpecialCollectionService : ISpecialCollectionService
    {


        private readonly AppDbContext _context;
        public SpecialCollectionService(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<SpecialCollection>> GetAllAsync()
        {
            return await _context.SpecialCollections.ToListAsync();
        }

        public async Task<SpecialCollection> GetByIdAsync(int? id)
        {
            return await _context.SpecialCollections.FirstOrDefaultAsync(s => s.Id == id);
        }
    }
}
