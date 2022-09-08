using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Entities
{
    public class SaleTranslation : BaseEntity, IAggregateRoot
    {
        public string HorizontalImage { get; set; }
        public string VerticalImage { get; set; }

        public int SaleId { get; set; }
        public int LanguageId { get; set; }

        [ForeignKey(nameof(SaleId))]
        public Sale Sale { get; set; }
        [ForeignKey(nameof(LanguageId))]
        public Language Language { get; set; }
    }
}
