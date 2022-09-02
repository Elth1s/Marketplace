namespace DAL.Entities
{
    public class DeliveryType : BaseEntity, IAggregateRoot
    {
        public string DarkIcon { get; set; }
        public string LightIcon { get; set; }
        public ICollection<Order> Orders { get; set; }
        public ICollection<Shop> Shops { get; set; }
        public ICollection<DeliveryTypeTranslation> DeliveryTypeTranslations { get; set; }
    }
}

