using Ardalis.Specification;
using DAL.Entities;

namespace WebAPI.Specifications.Shops
{
    public class ShopInfoFromProductSpecification : Specification<Shop>, ISingleResultSpecification<Shop>
    {
        public ShopInfoFromProductSpecification(int id)
        {
            Query.Where(a => a.Id == id)
                .Include(a => a.City).ThenInclude(c => c.CityTranslations)
                .Include(a => a.City).ThenInclude(c => c.Country).ThenInclude(c => c.CountryTranslations)
                .Include(p => p.Phones)
                .AsSplitQuery();
        }
    }
}
