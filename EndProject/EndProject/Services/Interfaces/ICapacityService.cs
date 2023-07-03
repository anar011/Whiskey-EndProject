using EndProject.Models;
using System.Drawing;

namespace EndProject.Services.Interfaces
{
    public interface ICapacityService
    {
        Task<IEnumerable<Capacity>> GetAllAsync();
        Task<Capacity> GetByIdAsync(int? id);
        bool CheckByName(string name);
    }
}
