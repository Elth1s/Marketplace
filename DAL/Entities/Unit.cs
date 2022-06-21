namespace DAL.Entities
{
    public class Unit : BaseEntity, IAggregateRoot
    {
        public string Measure { get; set; }

        public ICollection<FilterName> FilterNames { get; set; }
        public ICollection<CharacteristicName> CharacteristicNames { get; set; }
    }
}
