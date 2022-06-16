namespace DAL.Entities
{
    public class Unit : BaseEntity, IAggregateRoot
    {
        public string Measure { get; set; }

        public ICollection<FilterValue> FilterNames { get; set; }
    }
}
