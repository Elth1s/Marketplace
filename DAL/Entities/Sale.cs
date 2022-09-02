using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class Sale: BaseEntity, IAggregateRoot
    {

        public string Name { get; set; }
        public string Image { get; set; }

        public int DiscountMin { get; set; }
        public int DiscountMax { get; set; }

        public DateTime DateStart { get; set; }
        public DateTime DateEnd { get; set; }


        public ICollection<Product> Products { get; set; }

        public ICollection<Category> Categories { get; set; }
    }
}
