using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Entities
{
    public class FilterValue : BaseEntity, IAggregateRoot
    {
        public string Value { get; set; }

        public int? Min { get; set; }
        public int? Max { get; set; }

        public int FilterNameId { get; set; }

        [ForeignKey(nameof(FilterNameId))]
        public FilterName FilterName { get; set; }

        public ICollection<FilterValueProduct> FilterValueProducts { get; set; }
    }
}
