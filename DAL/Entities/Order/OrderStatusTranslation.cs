using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Entities
{
    public class OrderStatusTranslation : BaseEntity, IAggregateRoot
    {
        public string Name { get; set; }

        public int OrderStatusId { get; set; }
        public int LanguageId { get; set; }

        [ForeignKey(nameof(OrderStatusId))]
        public OrderStatus OrderStatus { get; set; }
        [ForeignKey(nameof(LanguageId))]
        public Language Language { get; set; }
    }
}
