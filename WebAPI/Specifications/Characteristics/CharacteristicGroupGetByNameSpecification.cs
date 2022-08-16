using Ardalis.Specification;
using DAL.Entities;

namespace WebAPI.Specifications.Characteristics
{
    public class CharacteristicGroupGetByNameSpecification : Specification<CharacteristicGroup>, ISingleResultSpecification<CharacteristicGroup>
    {
        public CharacteristicGroupGetByNameSpecification(string name, string userId)
        {
            Query.Where(item => name == item.Name && userId == item.UserId);
        }
    }
}
