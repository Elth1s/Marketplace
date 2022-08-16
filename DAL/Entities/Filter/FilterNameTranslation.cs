using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Entities
{
    public class FilterNameTranslation : BaseEntity, IAggregateRoot
    {
        public string Name { get; set; }

        public int FilterNameId { get; set; }
        public int LanguageId { get; set; }

        [ForeignKey(nameof(FilterNameId))]
        public FilterName FilterName { get; set; }
        [ForeignKey(nameof(LanguageId))]
        public Language Language { get; set; }
    }
}
