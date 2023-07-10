using EndProject.Helpers.Enums;

namespace EndProject.Models
{
    public class Order : BaseEntity
    {

        public string Address { get; set; }

        public decimal TotalPrice { get; set; }

        public Status Status { get; set; }
        public string Message { get; set; }
        public DateTime Date { get; set; }
        public string AppUserId { get; set; }
        public int Number { get; set; }

        public AppUser AppUser { get; set; }

        public List<OrderItem>? OrderItems { get; set; }
    }
}
