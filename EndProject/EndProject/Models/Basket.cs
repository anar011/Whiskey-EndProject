using EndProject.Helpers.Enums;

namespace EndProject.Models
{
    public class Basket:BaseEntity
    {
        public decimal TotalPrice { get; set; }

        public string AppUserID { get; set; }
        public AppUser AppUser { get; set; }

        public List<BasketItem> BasketItems { get; set; }

        public Status IsOrdered { get; set; }
        public Basket()
        {
            BasketItems = new();
        }

    }
}
