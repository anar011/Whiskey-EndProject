using EndProject.Models;

namespace EndProject.Services.Interfaces
{
    public interface IHomeAdvertisingService
    {
        Task<IEnumerable<HomeAddvertising>> GetAllAsync();
        Task<HomeAddvertising> GetByIdAsync(int? id);
    }
}
