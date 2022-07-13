using DAL.Entities.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities.Order
{

    public class Order : BaseEntity, IAggregateRoot
    {
        /// <summary>
        /// Контакти отримувача
        /// </summary>
        public string ConsumerFirstName { get; set; }
        public string ConsumerSecondName { get; set; }
        public string ConsumerPhone { get; set; }
        public string ConsumerEmail { get; set; }

        public int OrderStatusId { get; set; }
        public string UserId { get; set; }

        [ForeignKey(nameof(OrderStatusId))]
        public OrderStatus OrderStatus { get; set; }
        [ForeignKey(nameof(UserId))]
        public AppUser User { get; set; }

        public ICollection<OrderProduct> OrderProducts { get; set; }
    }
}
