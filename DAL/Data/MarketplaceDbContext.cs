using DAL.Entities;
using DAL.Entities.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DAL.Data
{
    public class MarketplaceDbContext : IdentityDbContext<AppUser>
    {
        public MarketplaceDbContext(DbContextOptions<MarketplaceDbContext> options) :
            base(options)
        {
        }

        public DbSet<CharacteristicGroup> CharacteristicGroups { get; set; }
        public DbSet<Characteristic> Characteristics { get; set; }

        public DbSet<FilterGroup> FilterGroups { get; set; }
        public DbSet<FilterName> FilterNames { get; set; }
        public DbSet<FilterValue> FilterValues { get; set; }
        public DbSet<FilterValueProduct> FilterValueProducts { get; set; }

        public DbSet<Product> Products { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }
        public DbSet<ProductStatus> ProductStatuses { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Country> Countries { get; set; }
        public DbSet<City> Cities { get; set; }

        public DbSet<Shop> Shops { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Category>()
                .HasOne(c => c.Parent)
                .WithMany(c => c.Children);

            builder.Entity<AppUser>()
                .HasOne(ap => ap.Shop)
                .WithOne(s => s.User);

        }
    }
}
