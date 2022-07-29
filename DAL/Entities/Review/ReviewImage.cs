using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Entities
{
    public class ReviewImage : BaseEntity, IAggregateRoot
    {
        public string Name { get; set; }

        public int? ReviewId { get; set; }

        [ForeignKey(nameof(ReviewId))]
        public Review Review { get; set; }
    }
}
