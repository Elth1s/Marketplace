using Ardalis.Specification;
using DAL.Entities;
using WebAPI.Extensions;

namespace WebAPI.Specifications.Countries
{
    public class CountrySearchSpecification : Specification<Country>
    {
        public CountrySearchSpecification(string name, bool isAscOrder, string orderBy)
        {
            if (!string.IsNullOrEmpty(name))
                Query.Where(item => item.Name.Contains(name));

            if (isAscOrder)
                Query.OrderBy(orderBy);
            else
                Query.OrderByDescending(orderBy);
        }
    }
}
