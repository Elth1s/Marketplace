using Ardalis.Specification;
using DAL.Entities;

namespace WebAPI.Specifications.Countries
{
    public class CountryGetByCodeSpecification : Specification<Country>, ISingleResultSpecification<Country>
    {
        public CountryGetByCodeSpecification(string code)
        {
            Query.Where(item => code == item.Code);
        }
    }
}
