namespace EndProject.Models
{
    public class BlogElementList : BaseEntity
    {
        public string Description { get; set; }

        public int BlogElementId { get; set; }
        public BlogElement BlogElement { get; set; }
    }
}
