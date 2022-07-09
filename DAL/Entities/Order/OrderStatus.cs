using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class OrderStatus : BaseEntity, IAggregateRoot
    {
        public string Name { get; set; }

        public ICollection<Order> Orders { get; set; }
    }
}
