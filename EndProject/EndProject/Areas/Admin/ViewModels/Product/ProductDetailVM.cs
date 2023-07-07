

using EndProject.Models;

namespace EndProject.Areas.Admin.ViewModels.Product
{
    public class ProductDetailVM
    {
        public int Id { get; set; }
        public string Image { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int SaleCount { get; set; }
        public int StockCount { get; set; }
    
        public ICollection<ProductCategory> CategoryNames { get; set; }
        public ICollection<ProductCapacity> CapacityNames { get; set; }

    }
}
