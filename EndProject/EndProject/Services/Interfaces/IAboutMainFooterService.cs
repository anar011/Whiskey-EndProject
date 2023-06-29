using EndProject.Models;

namespace EndProject.Services.Interfaces
{
    public interface IAboutMainFooterService
    {
        Task<IEnumerable<AboutMainFooter>> GetAllAsync();
        Task<AboutMainFooter> GetByIdAsync(int? id);
    }
}
