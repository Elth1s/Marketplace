using Ardalis.Specification;
using DAL.Entities;

namespace WebAPI.Specifications
{
    public class CityIncludeFullInfoSpecification : Specification<City>, ISingleResultSpecification<City>
    {
        public CityIncludeFullInfoSpecification()
        {
            Query.Include(o => o.Country)
                 .AsSplitQuery();
        }
        public CityIncludeFullInfoSpecification(int id)
        {
            Query.Where(o => o.Id == id)
                .Include(o => o.Country)
                .AsSplitQuery();
        }

    }
}
