using Ardalis.Specification;
using DAL.Entities;

namespace WebAPI.Specifications.Characteristics
{
    public class CharacteristicGroupGetByUserIdSpecification : Specification<CharacteristicGroup>
    {
        public CharacteristicGroupGetByUserIdSpecification(string userId)
        {
            Query.Where(item => userId == item.UserId);
        }
    }
}
