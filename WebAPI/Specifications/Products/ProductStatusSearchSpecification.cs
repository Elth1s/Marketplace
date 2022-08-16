using Ardalis.Specification;
using DAL.Entities;
using WebAPI.Extensions;
using WebAPI.Helpers;

namespace WebAPI.Specifications.Products
{
    public class ProductStatusSearchSpecification : Specification<ProductStatus>
    {
        public ProductStatusSearchSpecification(string name, bool isAscOrder, string orderBy, int? skip = null, int? take = null)
        {
            Query.Include(c => c.ProductStatusTranslations);
            if (!string.IsNullOrEmpty(name))
                Query.Where(item => item.ProductStatusTranslations.Any(c => c.LanguageId == CurrentLanguage.Id && c.Name.Contains(name)));

            if (orderBy == "name")
            {
                if (isAscOrder)
                    Query.OrderBy(c => c.ProductStatusTranslations.FirstOrDefault(t => t.LanguageId == CurrentLanguage.Id).Name);
                else
                    Query.OrderByDescending(c => c.ProductStatusTranslations.FirstOrDefault(t => t.LanguageId == CurrentLanguage.Id).Name);
            }
            else
            {
                if (isAscOrder)
                    Query.OrderBy(orderBy);
                else
                    Query.OrderByDescending(orderBy);
            }
            if (skip.HasValue)
                Query.Skip(skip.Value);

            if (take.HasValue)
                Query.Take(take.Value);
        }
    }
}
