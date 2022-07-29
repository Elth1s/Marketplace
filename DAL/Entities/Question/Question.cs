using DAL.Entities.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Entities
{
    public class Question : BaseEntity, IAggregateRoot
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Message { get; set; }
        public DateTime Date { get; set; }

        public string UserId { get; set; }
        public int ProductId { get; set; }

        public int CountLikes => Votes != null ? Votes.Count(r => r.IsLike) : 0;
        public int CountDislikes => Votes != null ? Votes.Count(r => !r.IsLike) : 0;

        [ForeignKey(nameof(UserId))]
        public AppUser User { get; set; }
        [ForeignKey(nameof(ProductId))]
        public Product Product { get; set; }

        public ICollection<QuestionImage> Images { get; set; }
        public ICollection<QuestionVotes> Votes { get; set; }
        public ICollection<QuestionReply> Replies { get; set; }

    }
}
