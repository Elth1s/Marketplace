using Ardalis.Specification;
using DAL.Entities;

namespace WebAPI.Specifications.Questions
{
    public class QuestionIncludeFullInfoSpecification : Specification<Question>, ISingleResultSpecification<Question>
    {

        public QuestionIncludeFullInfoSpecification(string userId)
        {
            Query.Where(_ => _.UserId == userId);
        }

    }
}
