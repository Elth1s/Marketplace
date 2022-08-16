using Ardalis.Specification;
using DAL.Entities;
using WebAPI.Extensions;
using WebAPI.Helpers;

namespace WebAPI.Specifications.Products
{
    public class ProductSearchSpecification : Specification<Product>
    {
        public ProductSearchSpecification(string name, bool isAscOrder, string orderBy, bool isSeller, int? shopId, int? skip = null, int? take = null)
        {
            if (!string.IsNullOrEmpty(name))
                if (shopId != null)
                    Query.Where(item => item.Name.Contains(name));

            if (isSeller)
                Query.Where(item => item.ShopId == shopId);

            Query.Include(o => o.Category).ThenInclude(c => c.CategoryTranslations)
                 .Include(o => o.Status).ThenInclude(s => s.ProductStatusTranslations)
                 .Include(pi => pi.Images);

            if (orderBy == "categoryName")
            {
                if (isAscOrder)
                    Query.OrderBy(c => c.Category.CategoryTranslations.FirstOrDefault(c => c.LanguageId == CurrentLanguage.Id).Name);
                else
                    Query.OrderByDescending(c => c.Category.CategoryTranslations.FirstOrDefault(c => c.LanguageId == CurrentLanguage.Id).Name);
            }
            else if (orderBy == "statusName")
            {
                if (isAscOrder)
                    Query.OrderBy(c => c.Status.ProductStatusTranslations.FirstOrDefault(
                                       c => c.LanguageId == CurrentLanguage.Id).Name);
                else
                    Query.OrderByDescending(c => c.Status.ProductStatusTranslations.FirstOrDefault(
                                            c => c.LanguageId == CurrentLanguage.Id).Name);
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
