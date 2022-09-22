using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Entities
{
    public class Shop : BaseEntity, IAggregateRoot
    {
        public string Name { get; set; }
        public string FullName { get; set; }
        public string Description { get; set; }
        public string Photo { get; set; }
        public string Email { get; set; }
        public string SiteUrl { get; set; }
        public bool IsDeleted { get; set; }

        public int? CountryId { get; set; }
        public int? CityId { get; set; }
        public string UserId { get; set; }

        [ForeignKey(nameof(CountryId))]
        public Country Country { get; set; }
        [ForeignKey(nameof(CityId))]
        public City City { get; set; }

        public AppUser User { get; set; }

        public ICollection<DeliveryType> DeliveryTypes { get; set; }
        public ICollection<ShopPhone> Phones { get; set; }
        public ICollection<Product> Products { get; set; }
        public ICollection<ShopReview> ShopReviews { get; set; }
        public ICollection<ShopScheduleItem> ShopSchedule { get; set; }

    }
}
