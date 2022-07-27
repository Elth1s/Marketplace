using Ardalis.Specification;
using DAL.Entities;
using WebAPI.Extensions;

namespace WebAPI.Specifications.Shops
{
    public class ShopSearchSpecification : Specification<Shop>
    {
        public ShopSearchSpecification(string name, bool isAscOrder, string orderBy, int? skip = null, int? take = null)
        {
            if (!string.IsNullOrEmpty(name))
                Query.Where(item => item.Name.Contains(name));

            Query.Include(o => o.City);

            if (orderBy == "cityName")
            {
                if (isAscOrder)
                    Query.OrderBy(c => c.City.Name);
                else
                    Query.OrderByDescending(c => c.City.Name);
            }
            else
            {
                if (isAscOrder)
                    Query.OrderBy(orderBy);
                else
                    Query.OrderByDescending(orderBy);
            }

            if (skip.HasValue)
                Query.Skip(skip.Value);

            if (take.HasValue)
                Query.Take(take.Value);
        }
    }
}
