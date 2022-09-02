namespace DAL.Entities
{
    public class DayOfWeek : BaseEntity, IAggregateRoot
    {
        public ICollection<DayOfWeekTranslation> DayOfWeekTranslations { get; set; }
        public ICollection<ShopScheduleItem> ShopScheduleItems { get; set; }
    }
}
