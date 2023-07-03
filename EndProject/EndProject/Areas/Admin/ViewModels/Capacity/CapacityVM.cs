using System.ComponentModel.DataAnnotations;

namespace EndProject.Areas.Admin.ViewModels.Capacity
{
    public class CapacityVM
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Don`t be empty")]
        public string Name { get; set; }
    }
}
