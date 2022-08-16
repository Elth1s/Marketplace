using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Entities
{
    public class Review : BaseEntity, IAggregateRoot
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public float ProductRating { get; set; }
        public DateTime Date { get; set; }
        public string Advantages { get; set; }
        public string Disadvantages { get; set; }
        public string Comment { get; set; }
        public string VideoURL { get; set; }

        public string UserId { get; set; }
        public int ProductId { get; set; }

        public int CountLikes => Votes != null ? Votes.Count(r => r.IsLike) : 0;
        public int CountDislikes => Votes != null ? Votes.Count(r => !r.IsLike) : 0;

        [ForeignKey(nameof(UserId))]
        public AppUser User { get; set; }
        [ForeignKey(nameof(ProductId))]
        public Product Product { get; set; }

        public ICollection<ReviewImage> Images { get; set; }
        public ICollection<ReviewVotes> Votes { get; set; }
        public ICollection<ReviewReply> Replies { get; set; }
    }
}
