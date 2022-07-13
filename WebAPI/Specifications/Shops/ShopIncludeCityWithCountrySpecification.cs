using Ardalis.Specification;
using DAL.Entities;

namespace WebAPI.Specifications.Shops
{
    public class ShopIncludeCityWithCountrySpecification : Specification<Shop>, ISingleResultSpecification<Shop>
    {
        public ShopIncludeCityWithCountrySpecification()
        {
            Query.Include(a => a.City)
                .ThenInclude(f => f.Country)
                .AsSplitQuery();
        }
        public ShopIncludeCityWithCountrySpecification(int id)
        {
            Query.Where(a => a.Id == id)
                .Include(u => u.User)
                .Include(a => a.City)
                .ThenInclude(c => c.Country)
                .AsSplitQuery();
        }
    }
}
