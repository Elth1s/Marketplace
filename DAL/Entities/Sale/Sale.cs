namespace DAL.Entities
{
    public class Sale : BaseEntity, IAggregateRoot
    {
        public string Name { get; set; }

        public int DiscountMin { get; set; }
        public int DiscountMax { get; set; }

        public DateTime DateStart { get; set; }
        public DateTime DateEnd { get; set; }

        public ICollection<Product> Products { get; set; }

        public ICollection<Category> Categories { get; set; }
        public ICollection<SaleTranslation> SaleTranslations { get; set; }
    }
}
