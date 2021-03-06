using Ardalis.Specification;
using DAL.Entities;

namespace WebAPI.Specifications.Orders
{

    public class OrderStatusGetByNameSpecification : Specification<OrderStatus>, ISingleResultSpecification<OrderStatus>
    {
        public OrderStatusGetByNameSpecification(string name)
        {
            Query.Where(item => name == item.Name);
        }
    }
}
