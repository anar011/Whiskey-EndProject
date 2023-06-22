using EndProject.Data;
using EndProject.Models;
using EndProject.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EndProject.Services
{
    public class SliderService : ISliderService
    {


        private readonly AppDbContext _context;
        public SliderService(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Slider>> GetAllAsync()
        {
            return await _context.Sliders.ToListAsync();
        }

        public async Task<Slider> GetByIdAsync(int? id)
        {
            return await _context.Sliders.FirstOrDefaultAsync(s => s.Id == id);
        }


    }
}
