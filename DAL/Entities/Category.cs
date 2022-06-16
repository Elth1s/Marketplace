using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Entities
{
    public class Category : BaseEntity, IAggregateRoot
    {
        public string Name { get; set; }
        public string Image { get; set; }

        public int? ParentId { get; set; }

        [ForeignKey(nameof(ParentId))]
        public Category Parent { get; set; }

        public ICollection<Category> Children { get; set; }
        public ICollection<Product> Products { get; set; }
        public ICollection<FilterValue> FiltersValue { get; set; }
    }
}
