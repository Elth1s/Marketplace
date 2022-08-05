using Ardalis.Specification;
using DAL.Entities;

namespace WebAPI.Specifications.Characteristics
{
    public class CharacteristicValueGetByValueSpecification : Specification<CharacteristicValue>, ISingleResultSpecification<CharacteristicValue>
    {
        public CharacteristicValueGetByValueSpecification(string value, int characteristicNameId, string userId)
        {
            Query.Where(item => value == item.Value && item.CharacteristicNameId == characteristicNameId && item.UserId == userId);
        }
    }
}
