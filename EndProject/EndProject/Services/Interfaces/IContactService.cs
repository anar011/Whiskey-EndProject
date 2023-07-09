using EndProject.Models;

namespace EndProject.Services.Interfaces
{
    public interface IContactService
    {
        Task<IEnumerable<Contact>> GetAllAsync();
        Task<Contact> GetByIdAsync(int? id);
    }
}
