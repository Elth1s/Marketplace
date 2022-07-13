using Ardalis.Specification;
using DAL.Entities;
using WebAPI.Extensions;

namespace WebAPI.Specifications.Filters
{
    public class FilterNameSearchSpecification : Specification<FilterName>
    {
        public FilterNameSearchSpecification(string name, bool isAscOrder, string orderBy)
        {
            if (!string.IsNullOrEmpty(name))
                Query.Where(item => item.Name.Contains(name));

            Query.Include(c => c.FilterGroup)
                .Include(c => c.Unit)
                .AsSplitQuery();

            if (orderBy == "filterGroupName")
            {
                if (isAscOrder)
                    Query.OrderBy(c => c.FilterGroup.Name);
                else
                    Query.OrderByDescending(c => c.FilterGroup.Name);
            }
            else if (orderBy == "unitMeasure")
            {
                if (isAscOrder)
                    Query.OrderBy(c => c.Unit.Measure);
                else
                    Query.OrderByDescending(c => c.Unit.Measure);
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
