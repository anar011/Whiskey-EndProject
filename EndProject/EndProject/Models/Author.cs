namespace EndProject.Models
{
    public class Author :BaseEntity
    {
        public string Image { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<Blog> Blogs { get; set; }


    }
}
