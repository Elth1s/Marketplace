using Ardalis.Specification;
using DAL.Entities;

namespace WebAPI.Specifications.Categories
{
    public class CategoryIncludeFullInfoSpecification : Specification<Category>, ISingleResultSpecification<Category>
    {
        public CategoryIncludeFullInfoSpecification()
        {
            Query.Include(p => p.Parent)
                 .ThenInclude(p => p.CategoryTranslations)
                 .Include(p => p.CategoryTranslations)
                 .AsSplitQuery();
        }

        public CategoryIncludeFullInfoSpecification(int? id)
        {
            Query.Where(o => o.Id == id)
                 .Include(p => p.CategoryTranslations)
                 .Include(p => p.Parent)
                 .ThenInclude(p => p.CategoryTranslations)
                 .AsSplitQuery();
        }
        public CategoryIncludeFullInfoSpecification(string urlSlug)
        {
            Query.Where(o => o.UrlSlug == urlSlug)
                 .Include(p => p.CategoryTranslations)
                 .Include(p => p.Parent)
                 .AsSplitQuery();
        }
    }
}
