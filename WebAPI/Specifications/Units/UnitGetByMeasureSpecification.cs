using Ardalis.Specification;
using DAL.Entities;

namespace WebAPI.Specifications.Units
{
    public class UnitGetByMeasureSpecification : Specification<Unit>, ISingleResultSpecification<Unit>
    {
        public UnitGetByMeasureSpecification(string measure, int languageId)
        {
            Query.Where(item => item.UnitTranslations.Any(t => t.LanguageId == languageId && t.Measure == measure));
        }
    }
}
