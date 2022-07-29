using Ardalis.Specification;
using DAL.Entities;

namespace WebAPI.Specifications.Questions
{
    public class QuestionVotesGetUserVoteSpecification : Specification<QuestionVotes>, ISingleResultSpecification<QuestionVotes>
    {
        public QuestionVotesGetUserVoteSpecification(int questionId, string userId)
        {
            Query.Where(r => r.QuestionId == questionId && r.UserId == userId);
        }
    }
}