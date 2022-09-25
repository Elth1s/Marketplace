using Ardalis.Specification;
using DAL.Entities;

namespace WebAPI.Specifications.Sales
{
    public class SaleIncludeFullInfoSpecification : Specification<Sale>, ISingleResultSpecification<Sale>
    {

        public SaleIncludeFullInfoSpecification()
        {
            Query.Include(p => p.Products)
                 .Where(p => p.Products.Count != 0)
                 .Include(s => s.SaleTranslations)
                 .Include(c => c.Categories)
                    .ThenInclude(c => c.CategoryTranslations);
        }

        public SaleIncludeFullInfoSpecification(int id)
        {
            Query.Where(i => i.Id == id)
                 .Include(s => s.SaleTranslations)
                 .Include(c => c.Categories)
                    .ThenInclude(c => c.CategoryTranslations);
        }

    }
}
