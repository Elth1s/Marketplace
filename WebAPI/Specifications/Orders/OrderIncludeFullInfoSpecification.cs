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
                 .Include(_ => _.DeliveryType).ThenInclude(_ => _.DeliveryTypeTranslations)
                 .AsSplitQuery();
        }

        public OrderIncludeFullInfoSpecification(int id)
        {
            Query.Where(_ => _.Id == id)
                 .Include(_ => _.OrderStatus)
                 .Include(_ => _.OrderProducts)
                    .ThenInclude(_ => _.Product)
                        .ThenInclude(_ => _.Images)
                 .Include(_ => _.OrderProducts)
                    .ThenInclude(_ => _.Product)
                        .ThenInclude(_ => _.Shop)
                 .Include(_ => _.DeliveryType)
                    .ThenInclude(_ => _.DeliveryTypeTranslations)
                 .AsSplitQuery();
        }



    }
}
