using Ardalis.Specification;
using DAL.Entities;

namespace WebAPI.Specifications.Characteristics
{
    public class CharacteristicNameGetByUserIdSpecification : Specification<CharacteristicName>
    {
        public CharacteristicNameGetByUserIdSpecification(string userId)
        {
            Query.Where(item => userId == item.UserId);
        }
    }
}
