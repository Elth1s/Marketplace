using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Entities
{
    public class GenderTranslation : BaseEntity, IAggregateRoot
    {
        public string Name { get; set; }

        public int GenderId { get; set; }
        public int LanguageId { get; set; }

        [ForeignKey(nameof(GenderId))]
        public Gender Gender { get; set; }
        [ForeignKey(nameof(LanguageId))]
        public Language Language { get; set; }
    }
}
