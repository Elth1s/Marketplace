using Ardalis.Specification;
using DAL.Entities;

namespace WebAPI.Specifications.Orders
{
    public class OrderIncludeFullInfoSpecification : Specification<Order>, ISingleResultSpecification<Order>
    {

        public OrderIncludeFullInfoSpecification()
        {
            Query.Include(_ => _.OrderStatus)
                .Include(_ => _.OrderProducts)
                .AsSplitQuery();
        }

        public OrderIncludeFullInfoSpecification(int id)
        {
            Query.Include(_ => _.Id == id)
                 .Include(_ => _.OrderStatus)
                 .Include(_ => _.OrderProducts)
                 .AsSplitQuery();
        }



    }
}
