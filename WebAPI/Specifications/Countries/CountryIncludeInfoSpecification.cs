using Ardalis.Specification;
using DAL.Entities;

namespace WebAPI.Specifications.Countries
{
    public class CountryIncludeInfoSpecification : Specification<Country>, ISingleResultSpecification<Country>
    {
        public CountryIncludeInfoSpecification()
        {
            Query.Include(c => c.CountryTranslations);
        }
        public CountryIncludeInfoSpecification(int id)
        {
            Query.Where(c => c.Id == id).Include(c => c.CountryTranslations);
        }
    }
}
