using EndProject.Models;

namespace EndProject.ViewModels.Home
{
    public class HomeVM
    {
        public IEnumerable<Slider> Sliders { get; set; }
        public SpecialCollection SpecialCollection { get; set; }

        public IEnumerable<HomeAddvertising> HomeAddvertisings { get; set; }

    }
}
