using Ardalis.Specification;
using DAL.Entities;
using WebAPI.Extensions;
using WebAPI.Helpers;

namespace WebAPI.Specifications.Countries
{
    public class CountrySearchSpecification : Specification<Country>
    {
        public CountrySearchSpecification(string name, bool isAscOrder, string orderBy, int? skip = null, int? take = null)
        {
            Query.Include(c => c.CountryTranslations);

            if (!string.IsNullOrEmpty(name))
                Query.Where(item => item.CountryTranslations.Any(c => c.LanguageId == CurrentLanguage.Id && c.Name.Contains(name)));

            if (orderBy == "name")
            {
                if (isAscOrder)
                    Query.OrderBy(c => c.CountryTranslations.FirstOrDefault(t => t.LanguageId == CurrentLanguage.Id).Name);
                else
                    Query.OrderByDescending(c => c.CountryTranslations.FirstOrDefault(t => t.LanguageId == CurrentLanguage.Id).Name);
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
