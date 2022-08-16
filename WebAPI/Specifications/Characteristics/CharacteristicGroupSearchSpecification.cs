using Ardalis.Specification;
using DAL.Entities;
using WebAPI.Extensions;

namespace WebAPI.Specifications.Characteristics
{
    public class CharacteristicGroupSearchSpecification : Specification<CharacteristicGroup>
    {
        public CharacteristicGroupSearchSpecification(string name, bool isAscOrder, string orderBy, bool isSeller, string userId, int? skip = null, int? take = null)
        {
            if (!string.IsNullOrEmpty(name))
                Query.Where(item => item.Name.Contains(name));

            if (isSeller)
                Query.Where(item => item.UserId == userId);

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
