using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Entities
{
    public class UnitTranslation : BaseEntity, IAggregateRoot
    {
        public string Measure { get; set; }

        public int UnitId { get; set; }
        public int LanguageId { get; set; }

        [ForeignKey(nameof(UnitId))]
        public Unit Unit { get; set; }
        [ForeignKey(nameof(LanguageId))]
        public Language Language { get; set; }
    }
}
