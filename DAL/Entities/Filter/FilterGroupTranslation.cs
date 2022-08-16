using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Entities
{
    public class FilterGroupTranslation : BaseEntity, IAggregateRoot
    {
        public string Name { get; set; }

        public int FilterGroupId { get; set; }
        public int LanguageId { get; set; }

        [ForeignKey(nameof(FilterGroupId))]
        public FilterGroup FilterGroup { get; set; }
        [ForeignKey(nameof(LanguageId))]
        public Language Language { get; set; }
    }
}
