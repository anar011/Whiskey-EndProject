using EndProject.Helpers;
using EndProject.Models;

namespace EndProject.ViewModels.Blog
{
    public class BlogVM
    {

        public List<Models.Blog> Blogs { get; set; }
        public Models.Blog Blog { get; set; }
        public Paginate<Models.Blog> PaginateDatas { get; set; }


        public Author Author { get; set; }
        public BlogInfo BlogInfo { get; set; }
        public BlogElement BlogElement { get; set; }

        public IEnumerable<BlogElementList> BlogElementLists { get; set; }


    }
}
 