using Ardalis.Specification;
using DAL.Entities;

namespace WebAPI.Specifications.Units
{
    public class UnitIncludeInfoSpecification : Specification<Unit>, ISingleResultSpecification<Unit>
    {
        public UnitIncludeInfoSpecification()
        {
            Query.Include(c => c.UnitTranslations);
        }
        public UnitIncludeInfoSpecification(int id)
        {
            Query.Where(c => c.Id == id).Include(c => c.UnitTranslations);
        }
    }
}
