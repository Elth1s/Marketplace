using Ardalis.Specification;
using DAL.Entities;

namespace WebAPI.Specifications
{
    public class GenderIncludeFullInfoSpecification : Specification<Gender>
    {
        public GenderIncludeFullInfoSpecification()
        {
            Query.Include(g => g.GenderTranslations);
        }
    }
}
