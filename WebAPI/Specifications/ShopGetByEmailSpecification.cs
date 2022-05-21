using Ardalis.Specification;
using DAL.Entities;

namespace WebAPI.Specifications
{
    public class ShopGetByEmailSpecification : Specification<Shop>, ISingleResultSpecification<Shop>
    {
        public ShopGetByEmailSpecification(string email)
        {
            Query.Where(item => email == item.Email);
        }
    }
}
