using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Entities
{
    public class ShopPhone : BaseEntity, IAggregateRoot
    {
        public string Phone { get; set; }
        public string Comment { get; set; }
        public int ShopId { get; set; }

        [ForeignKey(nameof(ShopId))]
        public Shop Shop { get; set; }
    }
}
