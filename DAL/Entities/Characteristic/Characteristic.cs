using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Entities
{
    public class Characteristic : BaseEntity, IAggregateRoot
    {
        public string Name { get; set; }

        public int CharacteristicGroupId { get; set; }

        [ForeignKey(nameof(CharacteristicGroupId))]
        public CharacteristicGroup CharacteristicGroup { get; set; }

        public ICollection<Product> Products { get; set; }
    }
}
