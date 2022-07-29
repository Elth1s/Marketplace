using Ardalis.Specification;
using DAL.Entities;

namespace WebAPI.Specifications.Reviews
{
    public class ReviewVotesGetUserVoteSpecification : Specification<ReviewVotes>, ISingleResultSpecification<ReviewVotes>
    {
        public ReviewVotesGetUserVoteSpecification(int reviewId, string userId)
        {
            Query.Where(r => r.ReviewId == reviewId && r.UserId == userId);
        }
    }
}