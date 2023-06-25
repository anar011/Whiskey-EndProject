using EndProject.Models;

namespace EndProject.Services.Interfaces
{
    public interface ISpecialCollectionService
    {
        Task<IEnumerable<SpecialCollection>> GetAllAsync();
        Task<SpecialCollection> GetByIdAsync(int? id);
    }
}
