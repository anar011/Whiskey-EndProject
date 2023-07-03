using EndProject.Helpers;
using EndProject.Models;
using EndProject.ViewModels.Product;
using System.Drawing;

namespace EndProject.ViewModels.Shop
{
    public class ShopVM
    {
        public IEnumerable<Models.Product> Products { get; set; }
        public IEnumerable<Category> Categories { get; set; }

        public Paginate<ProductVM> PaginateDatas { get; set; }

    }
}
