using Ardalis.Specification;
using DAL.Entities;

namespace WebAPI.Specifications.Categories
{
    public class CategoryGetWithoutChildrenSpecification : Specification<Category>
    {
        public CategoryGetWithoutChildrenSpecification()
        {
            Query.Include(item => item.Children)
                .Where(item => item.Children.Count == 0)
                .Include(item => item.CategoryTranslations)
                .AsSplitQuery();
        }
    }
}
