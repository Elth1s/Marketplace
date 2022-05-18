using Ardalis.Specification;
using DAL.Entities;

namespace WebAPI.Specifications
{
    public class CategoryIncludeFullInfoSpecification : Specification<Category>, ISingleResultSpecification<Category>
    {
        public CategoryIncludeFullInfoSpecification()
        {
            Query.Include(p => p.Parent)
                .Include(c => c.Characteristic)
                .AsSplitQuery();
        }

        public CategoryIncludeFullInfoSpecification(int id)
        {
            Query.Where(o => o.Id == id)
                .Include(p => p.Parent)
                .Include(c => c.Characteristic)
                .AsSplitQuery();
        }
    }
}
