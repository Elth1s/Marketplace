using Ardalis.Specification;
using DAL.Entities;

namespace WebAPI.Specifications.Reviews
{
    public class ReviewReplyByReviewIdSpecification : Specification<ReviewReply>
    {
        public ReviewReplyByReviewIdSpecification(int reviewId, int? skip = null, int? take = null)
        {
            Query.Where(r => r.ReviewId == reviewId)
                 .OrderByDescending(r => r.Date)
                 .AsSplitQuery();

            if (skip.HasValue)
                Query.Skip(skip.Value);

            if (take.HasValue)
                Query.Take(take.Value);
        }
    }
}
