using Ardalis.Specification;
using DAL.Entities;

namespace WebAPI.Specifications.Products
{
    public class ProductIncludeFullInfoSpecification : Specification<Product>, ISingleResultSpecification<Product>
    {
        public ProductIncludeFullInfoSpecification()
        {
            Query.Include(sh => sh.Shop)
                .Include(st => st.Status)
                .Include(c => c.Category)
                .Include(pi => pi.Images)
                .Include(pc => pc.FilterValueProducts)
                .Include(pc => pc.CharacteristicValues)
                .AsSplitQuery();
        }
        public ProductIncludeFullInfoSpecification(int id)
        {
            Query.Where(i => i.Id == id)
                .Include(sh => sh.Shop)
                .Include(st => st.Status)
                .Include(c => c.Category)
                .Include(pi => pi.Images)
                .Include(pc => pc.FilterValueProducts)
                .Include(pc => pc.CharacteristicValues)
                .AsSplitQuery();
        }

        public ProductIncludeFullInfoSpecification(string urlSlug)
        {
            Query.Where(i => i.UrlSlug.ToString() == urlSlug)
                .Include(sh => sh.Shop)
                .Include(st => st.Status)
                .Include(c => c.Category)
                .Include(pi => pi.Images)
                .Include(pc => pc.FilterValueProducts)
                .ThenInclude(p => p.FilterValue)
                .ThenInclude(p => p.FilterName)
                .ThenInclude(p => p.Unit)
                .Include(pc => pc.CharacteristicValues)
                .AsSplitQuery();
        }
    }
}
