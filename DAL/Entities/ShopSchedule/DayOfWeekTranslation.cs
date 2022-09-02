using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Entities
{
    public class DayOfWeekTranslation : BaseEntity, IAggregateRoot
    {
        public string Name { get; set; }
        public string ShortName { get; set; }

        public int DayOfWeekId { get; set; }
        public int LanguageId { get; set; }

        [ForeignKey(nameof(DayOfWeekId))]
        public DayOfWeek DayOfWeek { get; set; }
        [ForeignKey(nameof(LanguageId))]
        public Language Language { get; set; }
    }
}
