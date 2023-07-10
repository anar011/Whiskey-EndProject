using EndProject.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Drawing;

namespace EndProject.Data
{
    public class AppDbContext : IdentityDbContext<AppUser>
    {

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }


        public DbSet<Slider> Sliders { get;set; }
        public DbSet<SpecialCollection> SpecialCollections { get; set; }
        public DbSet<SectionBackgroundImage> SectionBackgroundImages { get; set; }
        public DbSet<Setting> Settings { get; set; }
        public DbSet<SectionHeader> SectionHeaders { get; set; }

        public DbSet<HomeAddvertising> HomeAddvertisings { get; set; }
        public DbSet<AboutUs> AboutUs { get; set; }
        public DbSet<AboutAdvertising> AboutAdvertisings { get; set; }
        public DbSet<AboutMainFooter> AboutMainFooters { get; set; }
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<BlogInfo> BlogInfos { get; set; }
        public DbSet<BlogElement> BlogElements { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<BlogElementList> BlogElementLists { get; set; }
        public DbSet<BlogAdvertising> BlogAdvertisings { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductComment> ProductComments { get; set; }
        public DbSet<Capacity> Capacities { get; set; }
        public DbSet<ProductCapacity> ProductCapacities { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }
        public DbSet<Basket> Baskets { get; set; }
        public DbSet<BasketItem> BasketItems { get; set; }
        public DbSet<WishlistItem> WishlistItems { get; set; }

        public DbSet<Contact> Contacts { get; set; }

        public DbSet<ContactInfo> ContactInfos { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Order> Orders { get; set; }







    }






}
