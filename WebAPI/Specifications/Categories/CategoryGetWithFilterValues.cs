using Ardalis.Specification;
using DAL.Entities;

namespace WebAPI.Specifications.Categories
{
    public class CategoryGetWithFilterValues : Specification<Category>, ISingleResultSpecification<Category>
    {
        public CategoryGetWithFilterValues(string urlSlug)
        {
            Query.Where(item => item.UrlSlug == urlSlug)
                 .Include(c => c.CategoryTranslations)
                 .Include(c => c.FilterValues).ThenInclude(c => c.FilterValueTranslations)
                 .Include(c => c.FilterValues).ThenInclude(f => f.FilterName).ThenInclude(f => f.FilterNameTranslations)
                 .Include(c => c.FilterValues).ThenInclude(f => f.FilterName).ThenInclude(f => f.Unit).ThenInclude(u => u.UnitTranslations)
                 .AsSplitQuery();
        }

        public CategoryGetWithFilterValues(int id)
        {
            Query.Where(item => item.Id == id)
                .Include(c => c.FilterValues).ThenInclude(c => c.FilterValueTranslations)
                .Include(c => c.FilterValues).ThenInclude(f => f.FilterName).ThenInclude(f => f.FilterNameTranslations)
                .Include(c => c.FilterValues).ThenInclude(f => f.FilterName).ThenInclude(f => f.FilterGroup).ThenInclude(f => f.FilterGroupTranslations)
                .Include(c => c.FilterValues).ThenInclude(f => f.FilterName).ThenInclude(f => f.Unit).ThenInclude(u => u.UnitTranslations)
                .AsSplitQuery();
        }
    }
}
