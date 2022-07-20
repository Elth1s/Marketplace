using Ardalis.Specification;
using DAL.Entities;
using WebAPI.Extensions;

namespace WebAPI.Specifications.Filters
{
    public class FilterGroupSearchSpecification : Specification<FilterGroup>
    {
        public FilterGroupSearchSpecification(string name, bool isAscOrder, string orderBy, int? skip = null, int? take = null)
        {
            if (!string.IsNullOrEmpty(name))
                Query.Where(item => item.Name.Contains(name));

            if (isAscOrder)
                Query.OrderBy(orderBy);
            else
                Query.OrderByDescending(orderBy);

            if (skip.HasValue)
                Query.Skip(skip.Value);

            if (take.HasValue)
                Query.Take(take.Value);
        }
    }
}
