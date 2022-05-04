using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Entities
{
    public class Filter : BaseEntity, IAggregateRoot
    {
        public string Name { get; set; }

        public int FilterGroupId { get; set; }

        [ForeignKey(nameof(FilterGroupId))]
        public FilterGroup FilterGroup { get; set; }

        public ICollection<Product> Products { get; set; }
    }
}
