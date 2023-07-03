using EndProject.Data;
using EndProject.Models;
using EndProject.Services.Interfaces;
using EndProject.ViewModels.Product;
using Microsoft.EntityFrameworkCore;
using System.Drawing;

namespace EndProject.Services
{
    public class ProductService : IProductService
    {

        private readonly AppDbContext _context;
        public ProductService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ProductVM>> GetDatasAsync()
        {
            List<ProductVM> model = new();
            var products = await _context.Products.ToListAsync();
            foreach (var item in products)
            {
                model.Add(new ProductVM
                {
                    Id = item.Id,
                    Price = item.Price,
                    Name = item.Name,
                   Image = item.Image,
                   
                });
            }
            return model;
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            return await _context.Products.ToListAsync();
        }


        public async Task<List<Product>> GetAllBySearchText(string searchText)
        {
            var products = await _context.Products
          .OrderByDescending(p => p.Id)
          .Where(p => p.Name.ToLower().Contains(searchText.ToLower()))
          .ToListAsync();
            return products;
        }

        public async Task<Product> GetByIdAsync(int? id)
        {
            return await _context.Products
                   .Include(p => p.ProductCategories)
                   .Include(p => p.ProductCapacities)
                   .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<ProductComment> GetCommentById(int? id)
        {
            return await _context.ProductComments.FirstOrDefaultAsync(pc => pc.Id == id);
        }

        public async Task<ProductComment> GetCommentByIdWithProduct(int? id)
        {
            return await _context.ProductComments.Include(p => p.Product).FirstOrDefaultAsync(pc => pc.Id == id);
        }

        public async Task<List<ProductComment>> GetComments()
        {
            return await _context.ProductComments.Include(p => p.Product).ToListAsync();
        }

        public async Task<int> GetCountAsync()
        {
            return await _context.Products.CountAsync();
        }

        public async Task<List<Product>> GetFullDataAsync()
        {
            return await _context.Products
                   
                     .Include(p => p.ProductComments)
                     .Include(p => p.ProductCategories)
                     .ThenInclude(pc => pc.Category)     
                     .Include(p => p.ProductCapacities)
                     .ThenInclude(ps => ps.Capacity)          
                     .ToListAsync();
        }

        public async Task<Product> GetFullDataByIdAsync(int? id)
        {
                return await _context.Products
               .Include(p=>p.ProductComments)
               .Include(p => p.ProductCategories)
               .ThenInclude(pc => pc.Category)
               .Include(p => p.ProductCapacities)
               .ThenInclude(ps => ps.Capacity)
               .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<List<Product>> GetPaginatedDatasAsync(int page, int take, int? categoryId)
        {
            if (categoryId != null)
            {
                return await _context.ProductCategories
                .Include(p => p.Product)
                .Where(pc => pc.Category.Id == categoryId)
                .Select(p => p.Product)
                .Skip((page * take) - take)
                .Take(take)
                .ToListAsync();
            }

            else
            {
                return await _context.Products
               .Include(p => p.ProductCategories)
               .ThenInclude(pc => pc.Category)
               .Include(p => p.ProductCapacities)
               .ThenInclude(ps => ps.Capacity)
               .Skip((page * take) - take)
               .Take(take)
               .ToListAsync();
            }

        }

        public async Task<List<ProductVM>> GetProductsByCategoryIdAsync(int? id, int page, int take)
        {
            List<ProductVM> model = new();
            var products = await _context.ProductCategories
                .Include(p => p.Product)
                .Where(pc => pc.Category.Id == id)
                .Select(p => p.Product)
                .Skip((page * take) - take)
                .Take(take)
                .ToListAsync();

            foreach (var item in products)
            {
                model.Add(new ProductVM
                {
                    Id = item.Id,
                    Price = item.Price,
                    Name = item.Name,
                    Image = item.Image,
             
                });
            }
            return model;
        }

        public async Task<int> GetProductsCountByCategoryAsync(int? id)
        {
                     return await _context.ProductCategories
                 .Include(p => p.Product)
                 .Where(pc => pc.Category.Id == id)
                 .Select(p => p.Product)
                 .CountAsync();
        }

        public async Task<IEnumerable<Product>> GetRelatedProducts()
        {
            return await _context.Products
            .ToListAsync();
        }
    }
}
