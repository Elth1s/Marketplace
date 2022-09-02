using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Entities
{
    public class ShopReview : BaseEntity, IAggregateRoot
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public int ServiceQualityRating { get; set; }
        public int TimelinessRating { get; set; }
        public int InformationRelevanceRating { get; set; }
        public string Comment { get; set; }
        public DateTime Date { get; set; }

        public string UserId { get; set; }
        public int ShopId { get; set; }


        [ForeignKey(nameof(UserId))]
        public AppUser User { get; set; }
        [ForeignKey(nameof(ShopId))]
        public Shop Shop { get; set; }
    }
}
