using Ardalis.Specification;
using DAL.Entities;

namespace WebAPI.Specifications.Orders
{
    public class DeliveryTypeIncludeInfoSpecification : Specification<DeliveryType>, ISingleResultSpecification<DeliveryType>
    {
        public DeliveryTypeIncludeInfoSpecification()
        {
            Query.Include(c => c.DeliveryTypeTranslations);
        }
        public DeliveryTypeIncludeInfoSpecification(int id)
        {
            Query.Where(c => c.Id == id)
                 .Include(c => c.DeliveryTypeTranslations);
        }
    }
}
