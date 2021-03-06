namespace DAL.Entities
{
    public class FilterGroup : BaseEntity, IAggregateRoot
    {
        public string Name { get; set; }

        public ICollection<FilterName> FiltersName { get; set; }
    }
}
