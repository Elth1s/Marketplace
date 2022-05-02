namespace DAL.Entities
{
    public class ProductStatus : BaseEntity, IAggregateRoot
    {
        public string Name { get; set; }
    }
}
