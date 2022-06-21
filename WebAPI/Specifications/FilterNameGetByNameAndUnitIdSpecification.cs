using Ardalis.Specification;
using DAL.Entities;

namespace WebAPI.Specifications
{
    public class FilterNameGetByNameAndUnitIdSpecification : Specification<FilterName>, ISingleResultSpecification<FilterName>
    {
        public FilterNameGetByNameAndUnitIdSpecification(string name, int? unitId)
        {
            Query.Where(item => name == item.Name && item.UnitId == unitId);
        }
    }
}
