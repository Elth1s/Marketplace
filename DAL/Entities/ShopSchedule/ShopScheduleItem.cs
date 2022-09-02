using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Entities
{
    public class ShopScheduleItem : BaseEntity, IAggregateRoot
    {
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public bool IsWeekend { get; set; }
        public int DayOfWeekId { get; set; }
        public int ShopId { get; set; }

        [ForeignKey(nameof(DayOfWeekId))]
        public DayOfWeek DayOfWeek { get; set; }
        [ForeignKey(nameof(ShopId))]
        public Shop Shop { get; set; }
    }
}
