using Ardalis.Specification;
using DAL.Entities;

namespace WebAPI.Specifications.Filters
{
    public class FilterGroupGetByNameSpecification : Specification<FilterGroup>, ISingleResultSpecification<FilterGroup>
    {
        public FilterGroupGetByNameSpecification(string name, int languageId)
        {
            Query.Include(c => c.FilterGroupTranslations)
                 .Where(item => item.FilterGroupTranslations.Any(t => t.LanguageId == languageId && t.Name == name));
        }
    }
}
