using Ardalis.Specification;
using DAL.Entities;
using WebAPI.Extensions;

namespace WebAPI.Specifications.Categories
{
    public class CategorySearchSpecification : Specification<Category>
    {
        public CategorySearchSpecification(string name, bool isAscOrder, string orderBy)
        {
            if (!string.IsNullOrEmpty(name))
                Query.Where(item => item.Name.Contains(name));

            Query.Include(o => o.Parent);

            if (orderBy == "parentName")
            {
                if (isAscOrder)
                    Query.OrderBy(c => c.Parent.Name);
                else
                    Query.OrderByDescending(c => c.Parent.Name);
            }
            else
            {
                if (isAscOrder)
                    Query.OrderBy(orderBy);
                else
                    Query.OrderByDescending(orderBy);
            }
        }
    }
}
