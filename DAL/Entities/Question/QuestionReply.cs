using DAL.Entities.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Entities
{
    public class QuestionReply : BaseEntity, IAggregateRoot
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Text { get; set; }
        public DateTime Date { get; set; }
        public bool IsSeller { get; set; }

        public string UserId { get; set; }
        public int QuestionId { get; set; }

        [ForeignKey(nameof(UserId))]
        public AppUser User { get; set; }

        [ForeignKey(nameof(QuestionId))]
        public Question Question { get; set; }
    }
}
