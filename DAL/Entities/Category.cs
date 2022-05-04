using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Entities
{
    public class Category : BaseEntity, IAggregateRoot
    {
        public string Name { get; set; }
        public string Image { get; set; }

        public int? ParentId { get; set; }
        public int CharacteristicId { get; set; }

        [ForeignKey(nameof(ParentId))]
        public Category Parent { get; set; }
        [ForeignKey(nameof(CharacteristicId))]
        public Characteristic Characteristic { get; set; }

        public ICollection<Category> Children { get; set; }
        public ICollection<Product> Products { get; set; }
        public ICollection<FilterGroup> FilterGroups { get; set; }
    }
}
