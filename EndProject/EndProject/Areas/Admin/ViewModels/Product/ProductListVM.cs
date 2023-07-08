using EndProject.Models;

namespace EndProject.Areas.Admin.ViewModels.Product
{
    public class ProductListVM
    {
        public int Id { get; set; }
        public string Image { get; set; }
        public string Name { get; set; }
        public List<ProductCapacity> ProductCapacities { get; set; }
    }
}
