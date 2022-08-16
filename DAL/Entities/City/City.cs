using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Entities
{
    public class City : BaseEntity, IAggregateRoot
    {
        public int CountryId { get; set; }

        [ForeignKey(nameof(CountryId))]
        public Country Country { get; set; }

        public ICollection<Shop> Shops { get; set; }
        public ICollection<CityTranslation> CityTranslations { get; set; }
    }
}
