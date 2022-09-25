using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Entities
{

    public class Order : BaseEntity, IAggregateRoot
    {
        public string ConsumerFirstName { get; set; }
        public string ConsumerSecondName { get; set; }
        public string ConsumerPhone { get; set; }
        public string ConsumerEmail { get; set; }
        public string TrackingNumber { get; set; }

        public string City { get; set; }
        public string Department { get; set; }

        public DateTime Date { get; set; }
        public int OrderStatusId { get; set; }
        public string UserId { get; set; }
        public int DeliveryTypeId { get; set; }

        [ForeignKey(nameof(OrderStatusId))]
        public OrderStatus OrderStatus { get; set; }

        [ForeignKey(nameof(UserId))]
        public AppUser User { get; set; }

        [ForeignKey(nameof(DeliveryTypeId))]
        public DeliveryType DeliveryType { get; set; }

        public ICollection<OrderProduct> OrderProducts { get; set; }
    }
}
