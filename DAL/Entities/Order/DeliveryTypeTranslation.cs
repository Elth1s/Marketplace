using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Entities
{
    public class DeliveryTypeTranslation : BaseEntity, IAggregateRoot
    {
        public string Name { get; set; }

        public int DeliveryTypeId { get; set; }
        public int LanguageId { get; set; }

        [ForeignKey(nameof(DeliveryTypeId))]
        public DeliveryType DeliveryType { get; set; }

        [ForeignKey(nameof(LanguageId))]
        public Language Language { get; set; }
    }
}
