using Ardalis.Specification;
using DAL.Entities;
using WebAPI.Extensions;

namespace WebAPI.Specifications.Orders
{
    public class OrderSearchSpecification : Specification<Order>, ISingleResultSpecification<Order>
    {
        public OrderSearchSpecification(string name, bool isAscOrder, string orderBy, bool isSeller, int? shopId, int? skip = null, int? take = null)
        {
            Query.Include(os => os.OrderStatus)
                 .Include(op => op.OrderProducts)
                 .ThenInclude(p => p.Product)
                 .AsSplitQuery();


            //if (id.HasValue)
            //    Query.Where(o => o.Id == id);


            if (isSeller)
                Query.Where(op => op.OrderProducts.Any(p => p.Product.ShopId == shopId));


            if (isAscOrder)
                Query.OrderBy(orderBy);
            else
                Query.OrderByDescending(orderBy);


            if (skip.HasValue)
                Query.Skip(skip.Value);

            if (take.HasValue)
                Query.Take(take.Value);
        }
    }
}
