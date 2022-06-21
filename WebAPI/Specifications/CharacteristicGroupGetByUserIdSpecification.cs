using Ardalis.Specification;
using DAL.Entities;

namespace WebAPI.Specifications
{
    public class CharacteristicGroupGetByUserIdSpecification : Specification<CharacteristicGroup>
    {
        public CharacteristicGroupGetByUserIdSpecification(string userId)
        {
            Query.Where(item => userId == item.UserId);
        }
    }
}
