namespace DAL.Entities
{
    public class Gender : BaseEntity, IAggregateRoot
    {
        public ICollection<GenderTranslation> GenderTranslations { get; set; }
        public ICollection<AppUser> Users { get; set; }
    }
}
