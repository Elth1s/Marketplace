using Ardalis.Specification;
using DAL.Entities;

namespace WebAPI.Specifications
{
    public class UnitGetByMeasureSpecification : Specification<Unit>, ISingleResultSpecification<Unit>
    {
        public UnitGetByMeasureSpecification(string measure)
        {
            Query.Where(item => measure == item.Measure);
        }
    }
}
