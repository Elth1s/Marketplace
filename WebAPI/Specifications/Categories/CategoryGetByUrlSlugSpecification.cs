using Ardalis.Specification;
using DAL.Entities;

namespace WebAPI.Specifications.Categories
{
    public class CategoryGetByUrlSlugSpecification : Specification<Category>, ISingleResultSpecification<Category>
    {
        public CategoryGetByUrlSlugSpecification(string urlSlug)
        {
            Query.Where(item => item.UrlSlug == urlSlug)
                 .Include(item => item.CategoryTranslations)
                 .AsSplitQuery();
        }
    }
}
