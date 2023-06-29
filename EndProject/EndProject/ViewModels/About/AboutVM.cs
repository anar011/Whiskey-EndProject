using EndProject.Models;

namespace EndProject.ViewModels.About
{
    public class AboutVM
    {
        public AboutUs AboutUs { get; set; }
        public IEnumerable<AboutAdvertising> AboutAdvertisings { get; set; }
        public AboutMainFooter AboutMainFooter { get; set; }

    }
}
