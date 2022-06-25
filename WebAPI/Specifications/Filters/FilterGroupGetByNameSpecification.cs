using Ardalis.Specification;
using DAL.Entities;

namespace WebAPI.Specifications.Filters
{
    public class FilterGroupGetByNameSpecification : Specification<FilterGroup>, ISingleResultSpecification<FilterGroup>
    {
        public FilterGroupGetByNameSpecification(string name)
        {
            Query.Where(item => name == item.Name);
        }
    }
}
