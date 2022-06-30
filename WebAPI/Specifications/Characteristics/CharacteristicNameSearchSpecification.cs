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

            if (isAscOrder)
                Query.OrderBy(orderBy);
            else
                Query.OrderByDescending(orderBy);

            Query.Include(c => c.CharacteristicGroup)
                .Include(c => c.Unit)
                .AsSplitQuery();
        }
    }
}
