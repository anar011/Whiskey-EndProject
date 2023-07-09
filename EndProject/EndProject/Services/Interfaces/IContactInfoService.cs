using EndProject.Models;

namespace EndProject.Services.Interfaces
{
    public interface IContactInfoService
    {
        Task<IEnumerable<ContactInfo>> GetAllAsync();
        Task<ContactInfo> GetByIdAsync(int? id);
    }
}
