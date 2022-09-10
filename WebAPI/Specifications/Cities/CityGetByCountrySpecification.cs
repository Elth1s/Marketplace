using Ardalis.Specification;
using DAL.Entities;

namespace WebAPI.Specifications.Cities
{
    public class CityGetByCountrySpecification : Specification<City>
    {
        public CityGetByCountrySpecification(int countryId)
        {
            Query.Where(c => c.CountryId == countryId)
                 .Include(c => c.CityTranslations);
        }
    }
}
