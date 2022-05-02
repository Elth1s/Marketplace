namespace DAL.Entities
{
    public class CharacteristicGroup : BaseEntity, IAggregateRoot
    {
        public string Name { get; set; }

        public ICollection<Characteristic> Characteristics { get; set; }
    }
}
