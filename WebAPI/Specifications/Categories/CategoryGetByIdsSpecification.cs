using Ardalis.Specification;
using DAL.Entities;
using System.Linq.Expressions;

namespace WebAPI.Specifications.Categories
{
    public class CategoryGetByIdsSpecification : Specification<Category>, ISingleResultSpecification<Category>
    {
        public CategoryGetByIdsSpecification(Expression<Func<Category, bool>> predicate)
        {
            Query.Where(predicate)
                 .AsSplitQuery();
        }
    }
}
