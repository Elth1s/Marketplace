namespace DAL.Entities
{
    public class Language : BaseEntity, IAggregateRoot
    {
        public string Name { get; set; }
        public string Culture { get; set; }

        public ICollection<CountryTranslation> CountryTranslations { get; set; }
        public ICollection<CityTranslation> CityTranslations { get; set; }
        public ICollection<UnitTranslation> UnitTranslations { get; set; }
        public ICollection<OrderStatusTranslation> OrderStatusTranslations { get; set; }
        public ICollection<ProductStatusTranslation> ProductStatusTranslations { get; set; }
        public ICollection<FilterGroupTranslation> FilterGroupTranslations { get; set; }
        public ICollection<FilterNameTranslation> FilterNameTranslations { get; set; }
        public ICollection<FilterValueTranslation> FilterValueTranslations { get; set; }
        public ICollection<CategoryTranslation> CategoryTranslations { get; set; }

    }
}
