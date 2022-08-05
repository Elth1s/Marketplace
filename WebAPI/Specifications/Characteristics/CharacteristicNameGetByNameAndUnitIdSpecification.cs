using Ardalis.Specification;
using DAL.Entities;

namespace WebAPI.Specifications.Characteristics
{
    public class CharacteristicNameGetByNameAndUnitIdSpecification : Specification<CharacteristicName>, ISingleResultSpecification<CharacteristicName>
    {
        public CharacteristicNameGetByNameAndUnitIdSpecification(string name, int? unitId, string userId)
        {
            Query.Where(item => name == item.Name && item.UnitId == unitId && item.UserId == userId);
        }
    }
}
