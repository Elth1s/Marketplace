using Ardalis.Specification;
using DAL.Entities;
using WebAPI.Extensions;

namespace WebAPI.Specifications.Cities
{
    public class CitySearchSpecification : Specification<City>
    {
        public CitySearchSpecification(string name, bool isAscOrder, string orderBy, int? skip = null, int? take = null)
        {
            if (!string.IsNullOrEmpty(name))
                Query.Where(item => item.Name.Contains(name));

            Query.Include(o => o.Country);

            if (orderBy == "countryName")
            {
                if (isAscOrder)
                    Query.OrderBy(c => c.Country.Name);
                else
                    Query.OrderByDescending(c => c.Country.Name);
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
