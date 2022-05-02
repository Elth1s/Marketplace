using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Entities
{
    public class ProductCharacteristic : BaseEntity, IAggregateRoot
    {
        public string Value { get; set; }

        public int CharacteristicId { get; set; }
        public int ProductId { get; set; }

        [ForeignKey(nameof(CharacteristicId))]
        public Characteristic Characteristic { get; set; }
        [ForeignKey(nameof(ProductId))]
        public Product Product { get; set; }
    }
}
