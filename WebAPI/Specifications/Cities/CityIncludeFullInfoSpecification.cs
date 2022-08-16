using Ardalis.Specification;
using DAL.Entities;

namespace WebAPI.Specifications.Cities
{
    public class CityIncludeFullInfoSpecification : Specification<City>, ISingleResultSpecification<City>
    {
        public CityIncludeFullInfoSpecification()
        {
            Query.Include(o => o.Country)
                 .ThenInclude(o => o.CountryTranslations)
                 .Include(o => o.CityTranslations)
                 .AsSplitQuery();
        }
        public CityIncludeFullInfoSpecification(int id)
        {
            Query.Where(o => o.Id == id)
                .Include(o => o.Country)
                .ThenInclude(o => o.CountryTranslations)
                .Include(o => o.CityTranslations)
                .AsSplitQuery();
        }

    }
}
