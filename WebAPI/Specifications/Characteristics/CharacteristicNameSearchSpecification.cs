using Ardalis.Specification;
using DAL.Entities;
using WebAPI.Extensions;

namespace WebAPI.Specifications.Characteristics
{
    public class CharacteristicNameSearchSpecification : Specification<CharacteristicName>
    {
        public CharacteristicNameSearchSpecification(string name, bool isAscOrder, string orderBy)
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
                    Query.OrderBy(c => c.Unit.Measure);
                else
                    Query.OrderByDescending(c => c.Unit.Measure);
            }
            else
            {
                if (isAscOrder)
                    Query.OrderBy(orderBy);
                else
                    Query.OrderByDescending(orderBy);
            }
        }
    }
}
