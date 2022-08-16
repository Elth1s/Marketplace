using Ardalis.Specification;
using DAL.Entities;

namespace WebAPI.Specifications.Categories
{
    public class CategoryGetByNameSpecification : Specification<Category>, ISingleResultSpecification<Category>
    {
        public CategoryGetByNameSpecification(string name, int languageId)
        {
            Query.Include(c => c.CategoryTranslations)
               .Where(item => item.CategoryTranslations.Any(t => t.LanguageId == languageId && t.Name == name));
        }
    }
}
