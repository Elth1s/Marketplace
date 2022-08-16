using Ardalis.Specification;
using DAL.Entities;

namespace WebAPI.Specifications.Cities
{
    public class CityGetByNameAndCountryIdSpecification : Specification<City>, ISingleResultSpecification<City>
    {
        public CityGetByNameAndCountryIdSpecification(string name, int countryId, int languageId)
        {
            Query.Include(c => c.CityTranslations)
                 .Where(item => item.CountryId == countryId &&
                                item.CityTranslations.Any(t => t.LanguageId == languageId && t.Name == name));
        }
    }
}
