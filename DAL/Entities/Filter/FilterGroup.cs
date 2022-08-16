namespace DAL.Entities
{
    public class FilterGroup : BaseEntity, IAggregateRoot
    {
        public ICollection<FilterGroupTranslation> FilterGroupTranslations { get; set; }
        public ICollection<FilterName> FiltersName { get; set; }
    }
}
