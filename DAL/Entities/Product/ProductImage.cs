using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Entities
{
    public class ProductImage : BaseEntity, IAggregateRoot
    {
        public string Name { get; set; }

        public int ProductId { get; set; }

        [ForeignKey(nameof(ProductId))]
        public Product Product { get; set; }
    }
}
