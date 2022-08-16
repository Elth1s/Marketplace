using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Entities
{
    public class CharacteristicName : BaseEntity, IAggregateRoot
    {
        public string Name { get; set; }

        public int CharacteristicGroupId { get; set; }
        public int? UnitId { get; set; }
        public string UserId { get; set; }


        [ForeignKey(nameof(CharacteristicGroupId))]
        public CharacteristicGroup CharacteristicGroup { get; set; }
        [ForeignKey(nameof(UnitId))]
        public Unit Unit { get; set; }
        [ForeignKey(nameof(UserId))]
        public AppUser User { get; set; }

        public ICollection<CharacteristicValue> CharacteristicValues { get; set; }
    }
}
