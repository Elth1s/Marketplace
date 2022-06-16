using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Entities
{
    public class FilterValueProduct : BaseEntity, IAggregateRoot
    {
        public float? CustomValue { get; set; }

        public int FilterValueId { get; set; }
        public int ProductId { get; set; }

        [ForeignKey(nameof(FilterValueId))]
        public FilterValue FilterValue { get; set; }
        [ForeignKey(nameof(ProductId))]
        public Product Product { get; set; }

    }
}
