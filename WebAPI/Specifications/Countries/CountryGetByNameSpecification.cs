using Ardalis.Specification;
using DAL.Entities;

namespace WebAPI.Specifications.Countries
{
    public class CountryGetByNameSpecification : Specification<Country>, ISingleResultSpecification<Country>
    {
        public CountryGetByNameSpecification(string name, int languageId)
        {
            Query.Include(c => c.CountryTranslations)
                 .Where(item => item.CountryTranslations.Any(t => t.LanguageId == languageId && t.Name == name));
        }
    }
}
