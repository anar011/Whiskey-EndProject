using EndProject.Models;

namespace EndProject.Services.Interfaces
{
    public interface IAboutUsService
    {
        Task<IEnumerable<AboutUs>> GetAllAsync();
        Task<AboutUs> GetByIdAsync(int? id);
    }
}
