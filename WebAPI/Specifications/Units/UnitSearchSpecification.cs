using Ardalis.Specification;
using DAL.Entities;
using WebAPI.Extensions;
using WebAPI.Helpers;

namespace WebAPI.Specifications.Units
{
    public class UnitSearchSpecification : Specification<Unit>
    {
        public UnitSearchSpecification(string measure, bool isAscOrder, string orderBy, int? skip = null, int? take = null)
        {
            Query.Include(c => c.UnitTranslations);

            if (!string.IsNullOrEmpty(measure))
                Query.Where(item => item.UnitTranslations.Any(
                            c => c.LanguageId == CurrentLanguage.Id && c.Measure.Contains(measure)));

            if (orderBy == "measure")
            {
                if (isAscOrder)
                    Query.OrderBy(c => c.UnitTranslations.FirstOrDefault(
                                  t => t.LanguageId == CurrentLanguage.Id).Measure);
                else
                    Query.OrderByDescending(c => c.UnitTranslations.FirstOrDefault(
                                            t => t.LanguageId == CurrentLanguage.Id).Measure);
            }

            if (isAscOrder)
                Query.OrderBy(orderBy);
            else
                Query.OrderByDescending(orderBy);

            if (skip.HasValue)
                Query.Skip(skip.Value);

            if (take.HasValue)
                Query.Take(take.Value);
        }
    }
}
