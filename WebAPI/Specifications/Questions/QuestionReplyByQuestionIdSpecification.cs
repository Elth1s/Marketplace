using Ardalis.Specification;
using DAL.Entities;

namespace WebAPI.Specifications.Questions
{
    public class QuestionReplyByQuestionIdSpecification : Specification<QuestionReply>
    {
        public QuestionReplyByQuestionIdSpecification(int questionId, int? skip = null, int? take = null)
        {
            Query.Where(r => r.QuestionId == questionId)
                 .OrderByDescending(r => r.Date)
                 .AsSplitQuery();

            if (skip.HasValue)
                Query.Skip(skip.Value);

            if (take.HasValue)
                Query.Take(take.Value);
        }
    }
}
