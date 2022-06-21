using Ardalis.Specification;
using DAL.Entities;

namespace WebAPI.Specifications
{
    public class CityGetByNameAndCountryIdSpecification : Specification<City>, ISingleResultSpecification<City>
    {
        public CityGetByNameAndCountryIdSpecification(string name, int countryId)
        {
            Query.Where(item => name == item.Name && item.CountryId == countryId);
        }
    }
}
