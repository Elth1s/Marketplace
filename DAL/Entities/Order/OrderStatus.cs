namespace DAL.Entities
{
    public class OrderStatus : BaseEntity, IAggregateRoot
    {
        public ICollection<Order> Orders { get; set; }
        public ICollection<OrderStatusTranslation> OrderStatusTranslations { get; set; }
    }
}
