using Ardalis.Specification;
using DAL.Entities;
using WebAPI.Extensions;
using WebAPI.Helpers;

namespace WebAPI.Specifications.Cities
{
    public class CitySearchSpecification : Specification<City>
    {
        public CitySearchSpecification(string name, bool isAscOrder, string orderBy, int? skip = null, int? take = null)
        {
            Query.Include(o => o.Country)
                 .ThenInclude(o => o.CountryTranslations)
                 .Include(o => o.CityTranslations);

            if (!string.IsNullOrEmpty(name))
                Query.Where(item => item.CityTranslations.FirstOrDefault(t => t.LanguageId == CurrentLanguage.Id).Name.Contains(name));

            if (orderBy == "name")
            {
                if (isAscOrder)
                    Query.OrderBy(c => c.CityTranslations.FirstOrDefault(t => t.LanguageId == CurrentLanguage.Id).Name);
                else
                    Query.OrderByDescending(c => c.CityTranslations.FirstOrDefault(t => t.LanguageId == CurrentLanguage.Id).Name);

            }

            else if (orderBy == "countryName")
            {
                if (isAscOrder)
                    Query.OrderBy(c => c.Country.CountryTranslations.FirstOrDefault(t => t.LanguageId == CurrentLanguage.Id).Name);
                else
                    Query.OrderByDescending(c => c.Country.CountryTranslations.FirstOrDefault(t => t.LanguageId == CurrentLanguage.Id).Name);
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
