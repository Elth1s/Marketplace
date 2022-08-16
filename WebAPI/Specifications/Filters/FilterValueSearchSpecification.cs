using Ardalis.Specification;
using DAL.Entities;
using WebAPI.Extensions;
using WebAPI.Helpers;

namespace WebAPI.Specifications.Filters
{
    public class FilterValueSearchSpecification : Specification<FilterValue>
    {
        public FilterValueSearchSpecification(string name, bool isAscOrder, string orderBy, int? skip = null, int? take = null)
        {
            Query.Include(c => c.FilterValueTranslations)
                 .Include(c => c.FilterName)
                 .ThenInclude(f => f.FilterNameTranslations);

            if (!string.IsNullOrEmpty(name))
                Query.Where(item => item.FilterValueTranslations.Any(c => c.LanguageId == CurrentLanguage.Id && c.Value.Contains(name)));

            Query.Include(c => c.FilterName)
                    .AsSplitQuery();

            if (orderBy == "name")
            {
                if (isAscOrder)
                    Query.OrderBy(c => c.FilterValueTranslations.FirstOrDefault(
                                  t => t.LanguageId == CurrentLanguage.Id).Value);
                else
                    Query.OrderByDescending(c => c.FilterValueTranslations.FirstOrDefault(
                                            t => t.LanguageId == CurrentLanguage.Id).Value);
            }
            else if (orderBy == "filterName")
            {
                if (isAscOrder)
                    Query.OrderBy(c => c.FilterName.FilterNameTranslations.FirstOrDefault(
                                  t => t.LanguageId == CurrentLanguage.Id).Name);
                else
                    Query.OrderByDescending(c => c.FilterName.FilterNameTranslations.FirstOrDefault(
                                  t => t.LanguageId == CurrentLanguage.Id).Name);
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
