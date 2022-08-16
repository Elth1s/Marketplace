namespace DAL.Entities
{
    public class ProductStatus : BaseEntity, IAggregateRoot
    {
        public ICollection<Product> Products { get; set; }
        public ICollection<ProductStatusTranslation> ProductStatusTranslations { get; set; }
    }
}
