using Ardalis.Specification;
using DAL.Entities;

namespace WebAPI.Specifications.Filters
{
    public class FilterGroupIncludeInfoSpecification : Specification<FilterGroup>, ISingleResultSpecification<FilterGroup>
    {
        public FilterGroupIncludeInfoSpecification()
        {
            Query.Include(c => c.FilterGroupTranslations);
        }
        public FilterGroupIncludeInfoSpecification(int id)
        {
            Query.Where(c => c.Id == id).Include(c => c.FilterGroupTranslations);
        }
    }
}
