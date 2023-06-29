using System.ComponentModel.DataAnnotations;

namespace EndProject.Areas.Admin.ViewModels.AboutMainFooter
{
    public class AboutMainFooterCreateVM
    {
        [Required(ErrorMessage = "Don`t be empty")]
        public IFormFile Photo { get; set; }

        [Required(ErrorMessage = "Don`t be empty")]
        public string Title { get; set; }


        [Required(ErrorMessage = "Don`t be empty")]
        public string Description { get; set; }
    }
}
