using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Entities
{
    public class CityTranslation : BaseEntity, IAggregateRoot
    {
        public string Name { get; set; }

        public int CityId { get; set; }
        public int LanguageId { get; set; }

        [ForeignKey(nameof(CityId))]
        public City City { get; set; }
        [ForeignKey(nameof(LanguageId))]
        public Language Language { get; set; }
    }
}
