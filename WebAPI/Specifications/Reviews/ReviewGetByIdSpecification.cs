using Ardalis.Specification;
using DAL.Entities;

namespace WebAPI.Specifications.Reviews
{
    public class ReviewGetByIdSpecification : Specification<Review>, ISingleResultSpecification<Review>
    {
        public ReviewGetByIdSpecification(int id)
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
