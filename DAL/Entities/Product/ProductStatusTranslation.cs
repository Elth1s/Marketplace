using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Entities
{
    public class ProductStatusTranslation : BaseEntity, IAggregateRoot
    {
        public string Name { get; set; }

        public int ProductStatusId { get; set; }
        public int LanguageId { get; set; }

        [ForeignKey(nameof(ProductStatusId))]
        public ProductStatus ProductStatus { get; set; }
        [ForeignKey(nameof(LanguageId))]
        public Language Language { get; set; }
    }
}
