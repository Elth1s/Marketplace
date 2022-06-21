namespace DAL.Entities
{
    public class Country : BaseEntity, IAggregateRoot
    {
        public string Name { get; set; }
        public string Code { get; set; }

        public ICollection<City> Cities { get; set; }
    }
}
