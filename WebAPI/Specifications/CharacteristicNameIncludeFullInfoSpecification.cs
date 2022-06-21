using Ardalis.Specification;
using DAL.Entities;

namespace WebAPI.Specifications
{
    public class CharacteristicNameIncludeFullInfoSpecification : Specification<CharacteristicName>, ISingleResultSpecification<CharacteristicName>
    {
        public CharacteristicNameIncludeFullInfoSpecification()
        {
            Query.Include(c => c.CharacteristicGroup)
                 .Include(c => c.Unit)
                 .AsSplitQuery();

        }

        public CharacteristicNameIncludeFullInfoSpecification(int id)
        {
            Query.Where(i => i.Id == id)
                .Include(c => c.CharacteristicGroup)
                .Include(c => c.Unit)
                .AsSplitQuery();

        }
    }
}
