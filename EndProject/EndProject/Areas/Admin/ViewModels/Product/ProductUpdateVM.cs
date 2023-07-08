using EndProject.Models;
using System.ComponentModel.DataAnnotations;

namespace EndProject.Areas.Admin.ViewModels.Product
{
    public class ProductUpdateVM
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Don`t be empty")]
        public IFormFile Photo { get; set; }
        public string Image { get; set; }

        [Required(ErrorMessage = "Don`t be empty")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Don`t be empty")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Don`t be empty")]
        public string Price { get; set; }

        [Required(ErrorMessage = "Don`t be empty")]
        public int SaleCount { get; set; }

        [Required(ErrorMessage = "Don`t be empty")]
        public int StockCount { get; set; }

        public ICollection<ProductCategory> CategoryNames { get; set; }
        public List<int> CategoryIds { get; set; } = new();

        public ICollection<ProductCapacity> CapacityNames { get; set; }

        public List<int> CapacityIds { get; set; } = new();
    }
}
