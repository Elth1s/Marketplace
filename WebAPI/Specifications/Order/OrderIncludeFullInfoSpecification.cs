using Ardalis.Specification;
using DAL.Entities;

namespace WebAPI.Specifications.Order
{
    public class OrderIncludeFullInfoSpecification: Specification<DAL.Entities.Order.Order>, ISingleResultSpecification<DAL.Entities.Order.Order>
    {

        public OrderIncludeFullInfoSpecification()
        {
            Query.Include(_ => _.OrderStatus)
                .Include(_ => _.OrderProducts)
                .AsSplitQuery();
        }

        public OrderIncludeFullInfoSpecification(int id)
        {
            Query.Include(_=>_.Id == id)
                 .Include(_ => _.OrderStatus)
                 .Include(_ => _.OrderProducts)
                 .AsSplitQuery();
        }



    }
}
