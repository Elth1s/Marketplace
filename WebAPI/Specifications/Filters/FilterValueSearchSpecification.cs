using Ardalis.Specification;
using DAL.Entities;
using WebAPI.Extensions;

namespace WebAPI.Specifications.Filters
{
    public class FilterValueSearchSpecification : Specification<FilterValue>
    {
        public FilterValueSearchSpecification(string name, bool isAscOrder, string orderBy)
        {
            if (!string.IsNullOrEmpty(name))
                Query.Where(item => item.Value.Contains(name));

            Query.Include(c => c.FilterName)
                .AsSplitQuery();

            if (orderBy == "filterName")
            {
                if (isAscOrder)
                    Query.OrderBy(c => c.FilterName.Name);
                else
                    Query.OrderByDescending(c => c.FilterName.Name);
            }
            else
            {
                if (isAscOrder)
                    Query.OrderBy(orderBy);
                else
                    Query.OrderByDescending(orderBy);
            }
        }

    }
}
