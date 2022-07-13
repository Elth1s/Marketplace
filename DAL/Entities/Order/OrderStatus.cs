namespace DAL.Entities
{
    public class OrderStatus : BaseEntity, IAggregateRoot
    {
        public string Name { get; set; }

        public ICollection<Order> Orders { get; set; }
    }
}
