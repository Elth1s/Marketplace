using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Entities
{
    public class FilterValueTranslation : BaseEntity, IAggregateRoot
    {
        public string Value { get; set; }
        public int FilterValueId { get; set; }
        public int LanguageId { get; set; }

        [ForeignKey(nameof(FilterValueId))]
        public FilterValue FilterValue { get; set; }
        [ForeignKey(nameof(LanguageId))]
        public Language Language { get; set; }
    }
}
