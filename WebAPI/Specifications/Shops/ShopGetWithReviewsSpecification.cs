using Ardalis.Specification;
using DAL.Entities;

namespace WebAPI.Specifications.Shops
{
    public class ShopGetWithReviewsSpecification : Specification<Shop>, ISingleResultSpecification<Shop>
    {
        public ShopGetWithReviewsSpecification(int shopId)
        {
            Query.Where(s => s.Id == shopId)
                 .Include(s => s.ShopReviews)
                 .Include(s => s.ShopSchedule)
                 .ThenInclude(s => s.DayOfWeek)
                 .ThenInclude(d => d.DayOfWeekTranslations);
        }
    }
}
