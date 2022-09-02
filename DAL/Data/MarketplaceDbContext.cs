using DAL.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using DayOfWeek = DAL.Entities.DayOfWeek;

namespace DAL.Data
{
    public class MarketplaceDbContext : IdentityDbContext<AppUser>
    {
        public MarketplaceDbContext(DbContextOptions<MarketplaceDbContext> options) :
            base(options)
        {
        }

        public DbSet<CharacteristicGroup> CharacteristicGroups { get; set; }
        public DbSet<CharacteristicName> CharacteristicNames { get; set; }
        public DbSet<CharacteristicValue> CharacteristicValues { get; set; }

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
        public DbSet<DayOfWeek> DaysOfWeek { get; set; }
        public DbSet<ShopScheduleItem> ShopScheduleItems { get; set; }

        public DbSet<BasketItem> BasketItems { get; set; }

        public DbSet<OrderStatus> OrderStatuses { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderProduct> OrderProducts { get; set; }
        public DbSet<DeliveryType> DeliveryTypes { get; set; }

        public DbSet<Unit> Units { get; set; }

        public DbSet<Question> Questions { get; set; }
        public DbSet<QuestionVotes> QuestionVotes { get; set; }
        public DbSet<QuestionReply> QuestionReplies { get; set; }
        public DbSet<QuestionImage> QuestionImages { get; set; }

        public DbSet<Review> Reviews { get; set; }
        public DbSet<ReviewVotes> ReviewVotes { get; set; }
        public DbSet<ReviewReply> ReviewReplies { get; set; }
        public DbSet<ReviewImage> ReviewImages { get; set; }

        public DbSet<Sale> Sales { get; set; }
        public DbSet<Language> Languages { get; set; }
        public DbSet<CountryTranslation> CountryTranslations { get; set; }
        public DbSet<CityTranslation> CityTranslations { get; set; }
        public DbSet<UnitTranslation> UnitTranslations { get; set; }
        public DbSet<OrderStatusTranslation> OrderStatusTranslations { get; set; }
        public DbSet<ProductStatusTranslation> ProductStatusTranslations { get; set; }
        public DbSet<FilterGroupTranslation> FilterGroupTranslations { get; set; }
        public DbSet<FilterNameTranslation> FilterNameTranslations { get; set; }
        public DbSet<FilterValueTranslation> FilterValueTranslations { get; set; }
        public DbSet<CategoryTranslation> CategoryTranslations { get; set; }
        public DbSet<DeliveryTypeTranslation> DeliveryTypeTranslations { get; set; }
        public DbSet<DayOfWeekTranslation> DayOfWeekTranslations { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Category>()
                .HasOne(c => c.Parent)
                .WithMany(c => c.Children);

            builder.Entity<AppUser>(entity =>
            {
                entity.HasIndex(u => u.PhoneNumber).IsUnique();
                entity.HasOne(ap => ap.Shop)
                      .WithOne(s => s.User)
                      .HasForeignKey<Shop>(s => s.UserId);
            });

            builder.Entity<Shop>()
                   .HasOne(s => s.User)
                   .WithOne(ap => ap.Shop)
                   .HasForeignKey<AppUser>(ap => ap.ShopId);

            builder.Entity<CountryTranslation>()
                   .HasIndex(c => new { c.CountryId, c.LanguageId })
                   .IsUnique();

            builder.Entity<CityTranslation>()
                   .HasIndex(c => new { c.CityId, c.LanguageId })
                   .IsUnique();

            builder.Entity<UnitTranslation>()
                   .HasIndex(c => new { c.UnitId, c.LanguageId })
                   .IsUnique();

            builder.Entity<OrderStatusTranslation>()
                   .HasIndex(c => new { c.OrderStatusId, c.LanguageId })
                   .IsUnique();

            builder.Entity<ProductStatusTranslation>()
                   .HasIndex(c => new { c.ProductStatusId, c.LanguageId })
                   .IsUnique();

            builder.Entity<FilterGroupTranslation>()
                   .HasIndex(c => new { c.FilterGroupId, c.LanguageId })
                   .IsUnique();

            builder.Entity<FilterNameTranslation>()
                   .HasIndex(c => new { c.FilterNameId, c.LanguageId })
                   .IsUnique();

            builder.Entity<FilterValueTranslation>()
                   .HasIndex(c => new { c.FilterValueId, c.LanguageId })
                   .IsUnique();

            builder.Entity<CategoryTranslation>()
                   .HasIndex(c => new { c.CategoryId, c.LanguageId })
                   .IsUnique();

            builder.Entity<DeliveryTypeTranslation>()
                   .HasIndex(c => new { c.DeliveryTypeId, c.LanguageId })
                   .IsUnique();

            builder.Entity<DayOfWeekTranslation>()
                   .HasIndex(c => new { c.DayOfWeekId, c.LanguageId })
                   .IsUnique();

            builder.Entity<BasketItem>().HasIndex(b => new { b.ProductId, b.UserId }).IsUnique();
        }
    }
}
