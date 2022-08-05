using Ardalis.Specification;
using DAL.Entities;
using WebAPI.Extensions;

namespace WebAPI.Specifications.Characteristics
{
    public class CharacteristicValueSearchSpecification : Specification<CharacteristicValue>
    {
        public CharacteristicValueSearchSpecification(string name, bool isAscOrder, string orderBy, bool isSeller, string userId, int? skip = null, int? take = null)
        {
            if (!string.IsNullOrEmpty(name))
                Query.Where(item => item.Value.Contains(name));

            if (isSeller)
                Query.Where(item => item.UserId == userId);

            Query.Include(c => c.CharacteristicName)
                .AsSplitQuery();

            if (orderBy == "characteristicName")
            {
                if (isAscOrder)
                    Query.OrderBy(c => c.CharacteristicName.Name);
                else
                    Query.OrderByDescending(c => c.CharacteristicName.Name);
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
