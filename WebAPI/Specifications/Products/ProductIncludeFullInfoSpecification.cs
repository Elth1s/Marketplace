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
                    .ThenInclude(s => s.ProductStatusTranslations)
                .Include(c => c.Category)
                    .ThenInclude(c => c.CategoryTranslations)
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
                    .ThenInclude(s => s.ProductStatusTranslations)
                .Include(c => c.Category)
                    .ThenInclude(c => c.CategoryTranslations)
                .Include(pi => pi.Images)
                .Include(pc => pc.FilterValueProducts)
                .Include(pc => pc.CharacteristicValues)
                .AsSplitQuery();
        }

        public ProductIncludeFullInfoSpecification(string urlSlug)
        {
            Query.Where(i => i.UrlSlug.ToString() == urlSlug)
                .Include(sh => sh.Shop)
                    .ThenInclude(s => s.ShopReviews)
                .Include(st => st.Status)
                    .ThenInclude(s => s.ProductStatusTranslations)
                .Include(c => c.Category)
                    .ThenInclude(c => c.CategoryTranslations)
                .Include(pi => pi.Images)
                .Include(pc => pc.FilterValueProducts)
                    .ThenInclude(p => p.FilterValue)
                        .ThenInclude(f => f.FilterValueTranslations)
                .Include(pc => pc.FilterValueProducts)
                    .ThenInclude(p => p.FilterValue)
                        .ThenInclude(p => p.FilterName)
                            .ThenInclude(f => f.FilterNameTranslations)
                .Include(pc => pc.FilterValueProducts)
                    .ThenInclude(p => p.FilterValue)
                        .ThenInclude(p => p.FilterName)
                            .ThenInclude(p => p.Unit)
                                .ThenInclude(u => u.UnitTranslations)
                .Include(pc => pc.CharacteristicValues)
                .Include(r => r.Reviews)
                .AsSplitQuery();
        }
    }
}
