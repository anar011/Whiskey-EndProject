using EndProject.Models;



namespace EndProject.ViewModels.Product
{
    public class ProductDetailVM
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public string Image { get; set; }
        public string Description { get; set; }

        public ICollection<ProductComment> ProductComments { get; set; }
        public ICollection<ProductCategory> ProductCategories { get; set; }
        public ICollection<ProductCapacity> ProductCapacities { get; set; }  
        public Dictionary<string, string> SectionBgs { get; set; }
        public IEnumerable<Models.Product> RelatedProducts { get; set; }
        public ProductCommentVM ProductCommentVM { get; set; }
    }
}
