using Ardalis.Specification;
using DAL.Entities;

namespace WebAPI.Specifications.Products
{
    public class ProductStatusGetByNameSpecification : Specification<ProductStatus>, ISingleResultSpecification<ProductStatus>
    {
        public ProductStatusGetByNameSpecification(string name, int languageId)
        {
            Query.Include(c => c.ProductStatusTranslations)
                  .Where(item => item.ProductStatusTranslations.Any(t => t.LanguageId == languageId && t.Name == name));
        }
    }
}
