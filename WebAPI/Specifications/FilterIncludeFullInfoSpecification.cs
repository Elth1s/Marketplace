using Ardalis.Specification;
using DAL.Entities;

namespace WebAPI.Specifications
{
    public class FilterIncludeFullInfoSpecification : Specification<Filter>, ISingleResultSpecification<Filter>
    {
        public FilterIncludeFullInfoSpecification()
        {
            Query.Include(o => o.FilterGroup);
        }
        public FilterIncludeFullInfoSpecification(int id)
        {
            Query.Where(o => o.Id == id)
                .Include(o => o.FilterGroup);
        }

    }
}
