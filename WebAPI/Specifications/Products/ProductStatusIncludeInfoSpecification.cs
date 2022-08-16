using Ardalis.Specification;
using DAL.Entities;

namespace WebAPI.Specifications.Products
{
    public class ProductStatusIncludeInfoSpecification : Specification<ProductStatus>, ISingleResultSpecification<ProductStatus>
    {
        public ProductStatusIncludeInfoSpecification()
        {
            Query.Include(c => c.ProductStatusTranslations);
        }
        public ProductStatusIncludeInfoSpecification(int id)
        {
            Query.Where(c => c.Id == id).Include(c => c.ProductStatusTranslations);
        }
    }
}
