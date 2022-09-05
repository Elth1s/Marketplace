using Ardalis.Specification;
using DAL.Entities;

namespace WebAPI.Specifications
{
    public class SaleIncludeFullInfoSpecification : Specification<Sale>, ISingleResultSpecification<Sale>
    {

        public SaleIncludeFullInfoSpecification()
        {
            Query.Include(p => p.Products)
                 .Include(c => c.Categories)
                    .ThenInclude(c => c.CategoryTranslations);
        }

        public SaleIncludeFullInfoSpecification(int id)
        {
            Query.Where(i => i.Id == id)
                 .Include(c => c.Categories)
                    .ThenInclude(c => c.CategoryTranslations);
        }

    }
}
