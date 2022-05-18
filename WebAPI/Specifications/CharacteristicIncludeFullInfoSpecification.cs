using Ardalis.Specification;
using DAL.Entities;

namespace WebAPI.Specifications
{
    public class CharacteristicIncludeFullInfoSpecification : Specification<Characteristic>, ISingleResultSpecification<Characteristic>
    {
        public CharacteristicIncludeFullInfoSpecification()
        {
            Query.Include(c => c.CharacteristicGroup);
        }

        public CharacteristicIncludeFullInfoSpecification(int id)
        {
            Query.Where(i => i.Id == id)
                .Include(c => c.CharacteristicGroup);
        }
    }
}
