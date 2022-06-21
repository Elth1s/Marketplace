using Ardalis.Specification;
using DAL.Entities;

namespace WebAPI.Specifications.Products
{
    public class ProductStatusGetByNameSpecification : Specification<ProductStatus>, ISingleResultSpecification<ProductStatus>
    {
        public ProductStatusGetByNameSpecification(string name)
        {
            Query.Where(item => name == item.Name);
        }
    }
}
