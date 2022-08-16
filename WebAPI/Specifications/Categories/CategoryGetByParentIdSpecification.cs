using Ardalis.Specification;
using DAL.Entities;

namespace WebAPI.Specifications.Categories
{
    public class CategoryGetByParentIdSpecification : Specification<Category>
    {
        public CategoryGetByParentIdSpecification(int parentId)
        {
            Query.Where(item => parentId == item.ParentId)
                 .Include(c => c.CategoryTranslations)
                 .AsSplitQuery();
        }
    }
}
