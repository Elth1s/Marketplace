using Ardalis.Specification;
using DAL.Entities;

namespace WebAPI.Specifications.Filters
{
    public class FilterNameGetByNameAndUnitIdSpecification : Specification<FilterName>, ISingleResultSpecification<FilterName>
    {
        public FilterNameGetByNameAndUnitIdSpecification(string name, int filterGroupId, int? unitId, int languageId)
        {
            Query.Include(c => c.FilterNameTranslations)
                 .Where(item => item.UnitId == unitId && item.FilterGroupId == filterGroupId &&
                               item.FilterNameTranslations.Any(t => t.LanguageId == languageId && t.Name == name));
        }
    }
}
