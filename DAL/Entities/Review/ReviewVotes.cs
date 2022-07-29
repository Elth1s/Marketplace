using DAL.Entities.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Entities
{
    public class ReviewVotes : BaseEntity, IAggregateRoot
    {
        public string UserId { get; set; }
        public int ReviewId { get; set; }
        public bool IsLike { get; set; }

        [ForeignKey(nameof(UserId))]
        public AppUser User { get; set; }
        [ForeignKey(nameof(ReviewId))]
        public Review Review { get; set; }
    }
}
