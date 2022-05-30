using Ardalis.Specification;
using DAL.Entities;

namespace WebAPI.Specifications
{
    public class ProductIncludeFullInfoSpecification : Specification<Product>, ISingleResultSpecification<Product>
    {
        public ProductIncludeFullInfoSpecification()
        {
            Query.Include(sh => sh.Shop)
                .Include(st => st.Status)
                .Include(c => c.Category)
                .Include(pi => pi.Images)
                .Include(pc => pc.Characteristics)
                .AsSplitQuery();
        }
        public ProductIncludeFullInfoSpecification(int id)
        {
            Query.Include(i => i.Id == id)
                .Include(sh => sh.Shop)
                .Include(st => st.Status)
                .Include(c => c.Category)
                .Include(pi => pi.Images)
                .Include(pc => pc.Characteristics)
                .AsSplitQuery();
        }
    }
}
