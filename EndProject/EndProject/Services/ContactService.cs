using EndProject.Data;
using EndProject.Models;
using EndProject.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EndProject.Services
{
    public class ContactService : IContactService
    {
        private readonly AppDbContext _context;

        public ContactService(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Contact>> GetAllAsync()
        {
            return await _context.Contacts.ToListAsync();
        }


        public async Task<Contact> GetByIdAsync(int? id)
        {
            return await _context.Contacts.FirstOrDefaultAsync(s => s.Id == id);
        }
    }
}
