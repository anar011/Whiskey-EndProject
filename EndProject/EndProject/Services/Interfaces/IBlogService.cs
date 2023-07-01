using EndProject.Models;

namespace EndProject.Services.Interfaces
{
    public interface IBlogService
    {


        Task<IEnumerable<Blog>> GetAllAsync();
        Task<Blog> GetByIdAsync(int? id);
        Task<BlogInfo> GetInfoById(int? id);
        Task<Author> GetAuthorById(int? id);
        Task<BlogElement> GetBlogElementById(int? id);
        Task<BlogElementList> GetBlogElementListById(int? id);
        Task<List<Blog>> GetPaginatedDatasAsync(int page, int take);
        Task<int> GetCountAsync();


    }
}
