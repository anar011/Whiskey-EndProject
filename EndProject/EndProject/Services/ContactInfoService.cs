using EndProject.Data;
using EndProject.Models;
using EndProject.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EndProject.Services
{
    public class ContactInfoService : IContactInfoService
    {
        private readonly AppDbContext _context;

        public ContactInfoService(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<ContactInfo>> GetAllAsync()
        {
            return await _context.ContactInfos.ToListAsync();
        }

        public async Task<ContactInfo> GetByIdAsync(int? id)
        {
            return await _context.ContactInfos.FirstOrDefaultAsync(s => s.Id == id);
        }
    }
}
