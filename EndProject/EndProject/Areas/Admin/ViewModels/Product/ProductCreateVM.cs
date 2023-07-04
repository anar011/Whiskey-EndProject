using System.ComponentModel.DataAnnotations;

namespace EndProject.Areas.Admin.ViewModels.Product
{
    public class ProductCreateVM
    {
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

        [Required(ErrorMessage = "Don`t be empty")]
        public IFormFile Photo { get; set; }

        [Required(ErrorMessage = "Don`t be empty")]
        public List<int> CategoryIds { get; set; } = new();

        [Required(ErrorMessage = "Don`t be empty")]
        public List<int> CapacityIds { get; set; } = new();
    }
}
