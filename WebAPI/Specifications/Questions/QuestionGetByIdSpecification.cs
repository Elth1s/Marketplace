using Ardalis.Specification;
using DAL.Entities;

namespace WebAPI.Specifications.Questions
{
    public class QuestionGetByIdSpecification : Specification<Question>, ISingleResultSpecification<Question>
    {
        public QuestionGetByIdSpecification(int id)
        {
            Query.Where(r => r.Id == id)
                .Include(r => r.Images)
                .Include(r => r.Votes)
                .Include(r => r.Replies)
                .OrderByDescending(r => r.Date)
                .AsSplitQuery();
        }
    }
}
