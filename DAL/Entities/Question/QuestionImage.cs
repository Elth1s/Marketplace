using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Entities
{
    public class QuestionImage : BaseEntity, IAggregateRoot
    {
        public string Name { get; set; }

        public int? QuestionId { get; set; }

        [ForeignKey(nameof(QuestionId))]
        public Question Question { get; set; }
    }
}
