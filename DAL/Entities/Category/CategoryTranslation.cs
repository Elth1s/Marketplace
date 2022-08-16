using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Entities
{
    public class CategoryTranslation : BaseEntity, IAggregateRoot
    {
        public string Name { get; set; }

        public int CategoryId { get; set; }
        public int LanguageId { get; set; }

        [ForeignKey(nameof(CategoryId))]
        public Category Category { get; set; }
        [ForeignKey(nameof(LanguageId))]
        public Language Language { get; set; }
    }
}
