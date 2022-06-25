using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Entities
{
    public class Product : BaseEntity, IAggregateRoot
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public float Price { get; set; }
        public int Count { get; set; }


        public int ShopId { get; set; }
        public int StatusId { get; set; }
        public int CategoryId { get; set; }

        [ForeignKey(nameof(ShopId))]
        public Shop Shop { get; set; }
        [ForeignKey(nameof(StatusId))]
        public ProductStatus Status { get; set; }
        [ForeignKey(nameof(CategoryId))]
        public Category Category { get; set; }

        public ICollection<BasketItem> BasketItems { get; set; }
        public ICollection<ProductImage> Images { get; set; }
        public ICollection<CharacteristicValue> CharacteristicValues { get; set; }
        public ICollection<FilterValueProduct> FilterValueProducts { get; set; }
    }
}
