using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Entities
{
    public class CountryTranslation : BaseEntity, IAggregateRoot
    {
        public string Name { get; set; }

        public int CountryId { get; set; }
        public int LanguageId { get; set; }

        [ForeignKey(nameof(CountryId))]
        public Country Country { get; set; }
        [ForeignKey(nameof(LanguageId))]
        public Language Language { get; set; }
    }
}
