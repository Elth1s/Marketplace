using Ardalis.Specification;
using DAL.Entities;

namespace WebAPI.Specifications
{
    public class CharacteristicValueIncludeFullInfoSpecification : Specification<CharacteristicValue>, ISingleResultSpecification<CharacteristicValue>
    {
        public CharacteristicValueIncludeFullInfoSpecification()
        {
            Query.Include(c => c.CharacteristicName)
                 .AsSplitQuery();

        }

        public CharacteristicValueIncludeFullInfoSpecification(int id)
        {
            Query.Where(i => i.Id == id)
                .Include(c => c.CharacteristicName)
                .AsSplitQuery();

        }
    }
}
