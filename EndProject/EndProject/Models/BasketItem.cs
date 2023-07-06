namespace EndProject.Models
{
    public class BasketItem:BaseEntity
    {
        public int ProductCapacityId { get; set; }
        public ProductCapacity ProductCapacity { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public int BasketId { get; set; }
        public Basket Basket { get; set; }


    }
}
