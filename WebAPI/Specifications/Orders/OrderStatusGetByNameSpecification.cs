using Ardalis.Specification;
using DAL.Entities;

namespace WebAPI.Specifications.Orders
{

    public class OrderStatusGetByNameSpecification : Specification<OrderStatus>, ISingleResultSpecification<OrderStatus>
    {
        public OrderStatusGetByNameSpecification(string name, int languageId)
        {
            Query.Include(c => c.OrderStatusTranslations)
                 .Where(item => item.OrderStatusTranslations.Any(t => t.LanguageId == languageId && t.Name == name));
        }
    }
}
