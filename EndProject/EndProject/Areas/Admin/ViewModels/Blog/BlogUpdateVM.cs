using System.ComponentModel.DataAnnotations;

namespace EndProject.Areas.Admin.ViewModels.Blog
{
    public class BlogUpdateVM
    { 
        public int Id { get; set; }
        [Required(ErrorMessage = "Don`t be empty")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Don`t be empty")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Don`t be empty")]
        public string Image { get; set; }
        public IFormFile Photo { get; set; }
    }
}
