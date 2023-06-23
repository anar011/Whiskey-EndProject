using EndProject.Models;

namespace EndProject.Services.Interfaces
{
    public interface ICrudService<T> where T : BaseEntity
    {
        Task CreateAsync(T entity);
        void Delete(T entity);
        Task SaveAsync();
        void Edit(T entity);
    }
}
