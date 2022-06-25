using Ardalis.Specification;
using DAL.Entities;

namespace WebAPI.Specifications
{
    public class CountryGetByNameSpecification : Specification<Country>, ISingleResultSpecification<Country>
    {
        public CountryGetByNameSpecification(string name)
        {
            Query.Where(item => name == item.Name);
        }
    }
}
