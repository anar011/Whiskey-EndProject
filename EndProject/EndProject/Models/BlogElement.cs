using System.Reflection.Metadata;

namespace EndProject.Models
{
    public class BlogElement : BaseEntity
    {
        public string Name { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int BlogId { get; set; }
        public Blog Blog { get; set; }
        public List<BlogElementList> BlogElementLists { get; set; }


    }
}
