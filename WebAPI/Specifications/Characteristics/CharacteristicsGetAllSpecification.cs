using Ardalis.Specification;
using DAL.Entities;

namespace WebAPI.Specifications.Characteristics
{
    public class CharacteristicsGetAllSpecification : Specification<CharacteristicValue>
    {
        public CharacteristicsGetAllSpecification(string userId)
        {
            Query.Where(item => item.UserId == userId)
                  .Include(c => c.CharacteristicName).ThenInclude(c => c.Unit).ThenInclude(c => c.UnitTranslations)
                  .Include(c => c.CharacteristicName).ThenInclude(c => c.CharacteristicGroup)
                  .AsSplitQuery();
        }
    }
}
