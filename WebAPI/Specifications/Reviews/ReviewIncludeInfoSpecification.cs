using Ardalis.Specification;
using DAL.Entities;

namespace WebAPI.Specifications.Reviews
{
    public class ReviewIncludeInfoSpecification : Specification<Review>
    {
        public ReviewIncludeInfoSpecification()
        {
            Query.Include(r => r.Images)
                 .Include(r => r.Votes)
                 .Include(r => r.Replies)
                 .OrderByDescending(r => r.Date)
                 .AsSplitQuery();
        }
    }
}
