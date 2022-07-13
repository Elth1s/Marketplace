using DAL.Entities.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Entities
{
    public class BasketItem : BaseEntity, IAggregateRoot
    {
        public int Count { get; set; }

        public string UserId { get; set; }
        public int ProductId { get; set; }

        [ForeignKey(nameof(UserId))]
        public AppUser User { get; set; }
        [ForeignKey(nameof(ProductId))]
        public Product Product { get; set; }
    }
}
