using System.ComponentModel.DataAnnotations.Schema;
namespace DAL.Entities
{
    public class OrderProduct : BaseEntity, IAggregateRoot
    {
        public int Count { get; set; }
        public float Price { get; set; }


        public int OrderId { get; set; }
        public int ProductId { get; set; }

        [ForeignKey(nameof(OrderId))]
        public Order Order { get; set; }
        [ForeignKey(nameof(ProductId))]
        public Product Product { get; set; }
    }
}