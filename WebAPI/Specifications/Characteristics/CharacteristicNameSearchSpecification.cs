using Ardalis.Specification;
using DAL.Entities;
using WebAPI.Extensions;
using WebAPI.Helpers;

namespace WebAPI.Specifications.Characteristics
{
    public class CharacteristicNameSearchSpecification : Specification<CharacteristicName>
    {
        public CharacteristicNameSearchSpecification(string name, bool isAscOrder, string orderBy, int? skip = null, int? take = null)
        {
            if (!string.IsNullOrEmpty(name))
                Query.Where(item => item.Name.Contains(name));

            Query.Include(c => c.CharacteristicGroup)
                .Include(c => c.Unit)
                .AsSplitQuery();

            if (orderBy == "characteristicGroupName")
            {
                if (isAscOrder)
                    Query.OrderBy(c => c.CharacteristicGroup.Name);
                else
                    Query.OrderByDescending(c => c.CharacteristicGroup.Name);
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
