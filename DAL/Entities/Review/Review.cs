using DAL.Entities.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Entities
{
    public class Review : BaseEntity, IAggregateRoot
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public float ProductRating { get; set; }
        public DateTime Date { get; set; }
        public string Advantage { get; set; }
        public string Disadvantage { get; set; }
        public string Comment { get; set; }
        public string VideoURL { get; set; }

        public string UserId { get; set; }
        public int ProductId { get; set; }

        [ForeignKey(nameof(UserId))]
        public AppUser User { get; set; }
        [ForeignKey(nameof(ProductId))]
        public Product Product { get; set; }

        public ICollection<AppUser> Likes { get; set; }
        public ICollection<AppUser> Dislikes { get; set; }

        public ICollection<ReviewImage> Images { get; set; }
    }
}
