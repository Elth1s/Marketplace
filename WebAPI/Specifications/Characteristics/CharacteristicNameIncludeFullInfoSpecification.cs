using Ardalis.Specification;
using DAL.Entities;

namespace WebAPI.Specifications.Characteristics
{
    public class CharacteristicNameIncludeFullInfoSpecification : Specification<CharacteristicName>, ISingleResultSpecification<CharacteristicName>
    {
        public CharacteristicNameIncludeFullInfoSpecification()
        {
            Query.Include(c => c.CharacteristicGroup)
                 .Include(c => c.Unit).ThenInclude(u => u.UnitTranslations)
                 .AsSplitQuery();

        }

        public CharacteristicNameIncludeFullInfoSpecification(int id)
        {
            Query.Where(i => i.Id == id)
                .Include(c => c.CharacteristicGroup)
                .Include(c => c.Unit).ThenInclude(u => u.UnitTranslations)
                .AsSplitQuery();

        }
    }
}
