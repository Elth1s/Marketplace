using Ardalis.Specification;
using DAL.Entities;

namespace WebAPI.Specifications.Orders
{
    public class DeliveryTypeGetByNameSpecification : Specification<DeliveryType>, ISingleResultSpecification<DeliveryType>
    {
        public DeliveryTypeGetByNameSpecification(string name, int languageId)
        {
            Query.Include(c => c.DeliveryTypeTranslations)
                 .Where(item => item.DeliveryTypeTranslations.Any(t => t.LanguageId == languageId && t.Name == name));
        }
    }
}
