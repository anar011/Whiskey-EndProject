using EndProject.Models;
using EndProject.ViewModels.Product;

namespace EndProject.Services.Interfaces
{
    public interface IProductService
    {

        Task<IEnumerable<Product>> GetAllAsync();
        Task<Product> GetByIdAsync(int? id);
        Task<List<Product>> GetFullDataAsync();
        Task<Product> GetFullDataByIdAsync(int? id);
        Task<int> GetCountAsync();
        Task<List<Product>> GetPaginatedDatasAsync(int page, int take, int? categoryId);
        Task<List<ProductVM>> GetProductsByCategoryIdAsync(int? id, int page, int take);
        Task<IEnumerable<ProductVM>> GetDatasAsync();
        Task<int> GetProductsCountByCategoryAsync(int? catId);
        Task<IEnumerable<Product>> GetRelatedProducts();
        Task<List<Product>> GetAllBySearchText(string searchText);
        Task<List<ProductComment>> GetComments();
        Task<ProductComment> GetCommentByIdWithProduct(int? id);
        Task<ProductComment> GetCommentById(int? id);
        Task<Product> GetDatasModalProductByIdAsyc(int? id);
    }
}
