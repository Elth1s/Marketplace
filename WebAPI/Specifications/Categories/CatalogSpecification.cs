using Ardalis.Specification;
using DAL.Entities;

namespace WebAPI.Specifications.Categories
{
    public class CatalogSpecification : Specification<Category>
    {
        public CatalogSpecification()
        {
            Query.Where(c => c.ParentId == null)
                .AsSplitQuery();
        }

        public CatalogSpecification(int? parentId)
        {
            Query.Where(c => c.ParentId == parentId)
                .Include(c => c.Childrens)
                .ThenInclude(c => c.Childrens)
                .AsSplitQuery();
        }
    }
}
