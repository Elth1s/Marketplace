using Ardalis.Specification;
using DAL.Entities;

namespace WebAPI.Specifications.Filters
{
    public class FilterValueIncludeFullInfoSpecification : Specification<FilterValue>, ISingleResultSpecification<FilterValue>
    {
        public FilterValueIncludeFullInfoSpecification()
        {
            Query.Include(o => o.FilterName)
                 .ThenInclude(f => f.FilterNameTranslations)
                 .Include(o => o.FilterValueTranslations)
                 .AsSplitQuery();
        }
        public FilterValueIncludeFullInfoSpecification(int id)
        {
            Query.Where(o => o.Id == id)
                .Include(o => o.FilterName)
                .ThenInclude(f => f.FilterNameTranslations)
                .Include(o => o.FilterValueTranslations)
                .AsSplitQuery();
        }

    }
}
