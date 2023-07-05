using System.Drawing;

namespace EndProject.Models
{
    public class Product : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }    
        public string Image { get; set; }
        public int SaleCount { get; set; }
        public int StockCount { get; set; }


        public ICollection<ProductCategory> ProductCategories { get; set; }
        public ICollection<ProductCapacity> ProductCapacities { get; set; }
        public ICollection<ProductComment> ProductComments { get; set; }
       
    }
}
