namespace DAL.Entities
{
    public class Unit : BaseEntity, IAggregateRoot
    {
        public ICollection<FilterName> FilterNames { get; set; }
        public ICollection<CharacteristicName> CharacteristicNames { get; set; }
        public ICollection<UnitTranslation> UnitTranslations { get; set; }
    }
}
