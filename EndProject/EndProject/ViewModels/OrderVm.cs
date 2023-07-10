using EndProject.Models;
using System.ComponentModel.DataAnnotations;

namespace EndProject.ViewModels
{
    public class OrderVm
    {
        [Required]
        [StringLength(maximumLength: 60)]
        public string Fullname { get; set; }
        [Required]
        [StringLength(maximumLength: 60)]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required]
        [StringLength(maximumLength: 100)]
        public string Address { get; set; }
        [Required]
        [StringLength(maximumLength: 100)]
        public string Message { get; set; }
        [Required]
        [DataType(DataType.PhoneNumber)]
        public int Number { get; set; }
        public List<BasketItem> BasketItems { get; set; }
        public OrderVm()
        {
            BasketItems = new();
        }
    }
}
