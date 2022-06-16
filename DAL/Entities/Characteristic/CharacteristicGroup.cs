using DAL.Entities.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Entities
{
    public class CharacteristicGroup : BaseEntity, IAggregateRoot
    {
        public string Name { get; set; }

        public string UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        public AppUser User { get; set; }

        public ICollection<Characteristic> Characteristics { get; set; }
    }
}
