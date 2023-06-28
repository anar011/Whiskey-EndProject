using EndProject.Models;

namespace EndProject.Services.Interfaces
{
    public interface IAboutAdvertisingService
    {
        Task<IEnumerable<AboutAdvertising>> GetAllAsync();
        Task<AboutAdvertising> GetByIdAsync(int? id);
    }
}
