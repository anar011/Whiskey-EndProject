using System.Reflection.Metadata;

namespace EndProject.Models
{
    public class BlogInfo : BaseEntity
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public int BlogId { get; set; }
        public Blog Blog { get; set; }
    }
}
