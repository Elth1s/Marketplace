using Ardalis.Specification;
using DAL.Entities;

namespace WebAPI.Specifications.Orders
{
    public class OrderStatusIncludeInfoSpecification : Specification<OrderStatus>, ISingleResultSpecification<OrderStatus>
    {
        public OrderStatusIncludeInfoSpecification()
        {
            Query.Include(c => c.OrderStatusTranslations);
        }
        public OrderStatusIncludeInfoSpecification(int id)
        {
            Query.Where(c => c.Id == id).Include(c => c.OrderStatusTranslations);
        }
    }
}
