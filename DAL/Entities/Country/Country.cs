namespace DAL.Entities
{
    public class Country : BaseEntity, IAggregateRoot
    {
        public string Code { get; set; }

        public ICollection<City> Cities { get; set; }
        public ICollection<CountryTranslation> CountryTranslations { get; set; }
    }
}
