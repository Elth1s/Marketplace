using Ardalis.Specification;
using DAL.Entities;

namespace WebAPI.Specifications.Characteristics
{
    public class CharacteristicNameGetByNameSpecification : Specification<CharacteristicName>, ISingleResultSpecification<CharacteristicName>
    {
        public CharacteristicNameGetByNameSpecification(int characteristicGroupId, string name, int? unitId, string userId)
        {
            Query.Where(item => characteristicGroupId == item.CharacteristicGroupId && name == item.Name && userId == item.UserId);

            if (unitId != null)
                Query.Where(item => unitId == item.UnitId);
        }
    }
}
