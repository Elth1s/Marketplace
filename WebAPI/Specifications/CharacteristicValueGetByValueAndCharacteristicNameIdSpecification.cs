using Ardalis.Specification;
using DAL.Entities;

namespace WebAPI.Specifications
{
    public class CharacteristicValueGetByValueAndCharacteristicNameIdSpecification : Specification<CharacteristicValue>, ISingleResultSpecification<CharacteristicValue>
    {
        public CharacteristicValueGetByValueAndCharacteristicNameIdSpecification(string value, int characteristicNameId)
        {
            Query.Where(item => value == item.Value && item.CharacteristicNameId == characteristicNameId);
        }
    }
}
