using Ardalis.Specification;
using DAL.Entities;

namespace WebAPI.Specifications.Orders
{
    public class OrderUserIncludeFullInfoSpecification : Specification<Order>, ISingleResultSpecification<Order>
    {

        public OrderUserIncludeFullInfoSpecification(string userId)
        {
            Query.Where(o => o.UserId == userId)
                 .Include(os => os.OrderStatus)
                 .Include(op => op.OrderProducts)
                 .ThenInclude(p => p.Product)
                 .ThenInclude(i => i.Images)
                 .Include(dt => dt.DeliveryType).ThenInclude(dtt => dtt.DeliveryTypeTranslations)
                 .AsSplitQuery();
        }
    }
}
