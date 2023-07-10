using EndProject.Helpers;
using EndProject.Models;
using EndProject.ViewModels.Product;

namespace EndProject.ViewModels.Home
{
    public class HomeVM
    {
        public IEnumerable<Slider> Sliders { get; set; }
        public SpecialCollection SpecialCollection { get; set; }
        public IEnumerable<HomeAddvertising> HomeAddvertisings { get; set; }
        public Paginate<ProductVM> PaginateDatas { get; set; }
        public IEnumerable<Models.Product> Products { get; set; }


    }
}
