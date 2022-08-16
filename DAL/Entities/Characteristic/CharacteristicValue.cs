using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Entities
{
    public class CharacteristicValue : BaseEntity, IAggregateRoot
    {
        public string Value { get; set; }


        public int CharacteristicNameId { get; set; }
        public string UserId { get; set; }


        [ForeignKey(nameof(CharacteristicNameId))]
        public CharacteristicName CharacteristicName { get; set; }
        [ForeignKey(nameof(UserId))]
        public AppUser User { get; set; }


        public ICollection<Product> Products { get; set; }
    }
}
