using System.Drawing;

namespace EndProject.Models
{
    public class ProductCapacity:BaseEntity
    {
        public decimal Price { get; set; }
        public int ProductId { get; set; }
        public int CapacityId { get; set; }
        public Product Product { get; set; }
        public Capacity Capacity { get; set; }
    }

}
