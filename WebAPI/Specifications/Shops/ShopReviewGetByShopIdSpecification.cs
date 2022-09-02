using Ardalis.Specification;
using DAL.Entities;

namespace WebAPI.Specifications.Shops
{
    public class ShopReviewGetByShopIdSpecification : Specification<ShopReview>
    {
        public ShopReviewGetByShopIdSpecification(int shopId, int? skip = null, int? take = null)
        {
            Query.Where(s => s.ShopId == shopId)
                 .OrderByDescending(s => s.Date);

            if (skip.HasValue)
                Query.Skip(skip.Value);

            if (take.HasValue)
                Query.Take(take.Value);
        }
    }
}
