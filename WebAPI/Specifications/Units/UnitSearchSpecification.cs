using Ardalis.Specification;
using DAL.Entities;
using WebAPI.Extensions;

namespace WebAPI.Specifications.Units
{
    public class UnitSearchSpecification : Specification<Unit>
    {
        public UnitSearchSpecification(string measure, bool isAscOrder, string orderBy)
        {
            if (!string.IsNullOrEmpty(measure))
                Query.Where(item => item.Measure.Contains(measure));

            if (isAscOrder)
                Query.OrderBy(orderBy);
            else
                Query.OrderByDescending(orderBy);
        }
    }
}
