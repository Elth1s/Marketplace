using Ardalis.Specification;
using DAL.Entities;
using System.Linq.Expressions;

namespace WebAPI.Specifications.Filters
{
    public class FilterValuesGetByIdsSpecification : Specification<FilterValue>, ISingleResultSpecification<FilterValue>
    {
        public FilterValuesGetByIdsSpecification(Expression<Func<FilterValue, bool>> predicate)
        {
            Query.Where(predicate)
                .OrderBy(f => f.FilterNameId)
                 .AsSplitQuery();
        }
    }
}
