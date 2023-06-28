using EndProject.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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









    }




}
