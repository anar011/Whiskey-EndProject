namespace EndProject.Models
{
    public class OrderItem :BaseEntity
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int? ProductId { get; set; }
        public int Quantity { get; set; }

        public int OrderId { get; set; }

        public Product Product { get; set; }

        public Order Order { get; set; }

        public AppUser AppUser { get; set; }
    }
}
