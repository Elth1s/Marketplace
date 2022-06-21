using Ardalis.Specification;
using DAL.Entities;

namespace WebAPI.Specifications
{
    public class ProductImageGetByProductSpecification : Specification<ProductImage>
    {
        public ProductImageGetByProductSpecification(int productId)
        {
            Query.Where(item => productId == item.ProductId);
        }
    }
}
