using Ardalis.Specification;
using DAL.Entities;

namespace WebAPI.Specifications
{
    public class FilterValueIncludeFullInfoSpecification : Specification<FilterValue>, ISingleResultSpecification<FilterValue>
    {
        public FilterValueIncludeFullInfoSpecification()
        {
            Query.Include(o => o.FilterName);
        }
        public FilterValueIncludeFullInfoSpecification(int id)
        {
            Query.Where(o => o.Id == id)
                .Include(o => o.FilterName);
        }

    }
}
