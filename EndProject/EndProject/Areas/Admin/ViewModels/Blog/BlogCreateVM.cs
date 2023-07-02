using System.ComponentModel.DataAnnotations;

namespace EndProject.Areas.Admin.ViewModels.Blog
{
    public class BlogCreateVM
    {
        [Required(ErrorMessage = "Don`t be empty")]
        public IFormFile Photo { get; set; }

        [Required(ErrorMessage = "Don`t be empty")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Don`t be empty")]
        public string Description { get; set; }


        [Required(ErrorMessage = "Don`t be empty")]
        public List<int> AuthorId { get; set; } = new();
    }
}
