using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Entities
{
    public class QuestionVotes : BaseEntity, IAggregateRoot
    {
        public string UserId { get; set; }
        public int QuestionId { get; set; }
        public bool IsLike { get; set; }

        [ForeignKey(nameof(UserId))]
        public AppUser User { get; set; }
        [ForeignKey(nameof(QuestionId))]
        public Question Question { get; set; }
    }
}
