using Ardalis.Specification;
using DAL.Entities;

namespace WebAPI.Specifications.Categories
{
    public class CategoryGetByNameSpecification : Specification<Category>, ISingleResultSpecification<Category>
    {
        public CategoryGetByNameSpecification(string name)
        {
            Query.Where(item => name == item.Name)
                .AsSplitQuery();
        }
    }
}
