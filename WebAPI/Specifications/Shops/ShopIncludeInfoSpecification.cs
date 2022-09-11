using Ardalis.Specification;
using DAL.Entities;

namespace WebAPI.Specifications.Shops
{
    public class ShopIncludeInfoSpecification : Specification<Shop>, ISingleResultSpecification<Shop>
    {
        public ShopIncludeInfoSpecification(int id)
        {
            Query.Where(s => s.Id == id)
                 .Include(a => a.City).ThenInclude(c => c.CityTranslations)
                 .Include(a => a.City).ThenInclude(f => f.Country).ThenInclude(c => c.CountryTranslations)
                 .Include(a => a.Phones)
                 .AsSplitQuery();
        }
    }
}
