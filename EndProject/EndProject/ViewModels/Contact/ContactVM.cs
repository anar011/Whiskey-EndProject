using System.ComponentModel.DataAnnotations;

namespace EndProject.ViewModels.Contact
{
    public class ContactVM
    {
        [Required(ErrorMessage = "Don`t be empty")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Don`t be empty")]
        public string Subject { get; set; }
        [Required(ErrorMessage = "Don`t be empty")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        public string Message { get; set; }
    }
}
