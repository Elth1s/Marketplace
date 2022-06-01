using Ardalis.Specification;
using DAL.Entities;

namespace WebAPI.Specifications
{
    public class ProductCharacteristicGetByProductSpecification : Specification<ProductCharacteristic>, ISingleResultSpecification<ProductCharacteristic>
    {
        public ProductCharacteristicGetByProductSpecification(int productId)
        {
            Query.Where(item => productId == item.ProductId);
        }
    }
}
