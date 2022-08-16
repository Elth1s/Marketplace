using Ardalis.Specification;
using DAL.Entities;
using WebAPI.Extensions;
using WebAPI.Helpers;

namespace WebAPI.Specifications.Filters
{
    public class FilterNameSearchSpecification : Specification<FilterName>
    {
        public FilterNameSearchSpecification(string name, bool isAscOrder, string orderBy, int? skip = null, int? take = null)
        {
            Query.Include(c => c.FilterNameTranslations)
                 .Include(c => c.FilterGroup)
                 .ThenInclude(f => f.FilterGroupTranslations)
                 .Include(c => c.Unit)
                 .ThenInclude(u => u.UnitTranslations)
                 .AsSplitQuery();

            if (!string.IsNullOrEmpty(name))
                Query.Where(item => item.FilterNameTranslations.Any(c => c.LanguageId == CurrentLanguage.Id && c.Name.Contains(name)));

            if (orderBy == "name")
            {
                if (isAscOrder)
                    Query.OrderBy(c => c.FilterNameTranslations.FirstOrDefault(
                                  t => t.LanguageId == CurrentLanguage.Id).Name);
                else
                    Query.OrderByDescending(c => c.FilterNameTranslations.FirstOrDefault(
                                            t => t.LanguageId == CurrentLanguage.Id).Name);
            }
            else if (orderBy == "filterGroupName")
            {
                if (isAscOrder)
                    Query.OrderBy(c => c.FilterGroup.FilterGroupTranslations.FirstOrDefault(
                                  t => t.LanguageId == CurrentLanguage.Id).Name);
                else
                    Query.OrderByDescending(c => c.FilterGroup.FilterGroupTranslations.FirstOrDefault(
                                  t => t.LanguageId == CurrentLanguage.Id).Name);
            }
            else if (orderBy == "unitMeasure")
            {
                if (isAscOrder)
                    Query.OrderBy(c => c.Unit.UnitTranslations.FirstOrDefault(
                                  t => t.LanguageId == CurrentLanguage.Id).Measure);
                else
                    Query.OrderByDescending(c => c.Unit.UnitTranslations.FirstOrDefault(
                                  t => t.LanguageId == CurrentLanguage.Id).Measure);
            }
            else
            {
                if (isAscOrder)
                    Query.OrderBy(orderBy);
                else
                    Query.OrderByDescending(orderBy);
            }

            if (skip.HasValue)
                Query.Skip(skip.Value);

            if (take.HasValue)
                Query.Take(take.Value);
        }
    }
}
