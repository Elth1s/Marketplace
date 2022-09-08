using Ardalis.Specification;
using DAL.Entities;
using WebAPI.Extensions;
using WebAPI.Helpers;

namespace WebAPI.Specifications.Sales
{
    public class SaleSearchSpecification : Specification<Sale>
    {
        public SaleSearchSpecification(string name, bool isAscOrder, string orderBy, int? skip = null, int? take = null)
        {
            Query.Include(c => c.SaleTranslations);

            if (!string.IsNullOrEmpty(name))
                Query.Where(item => item.Name.Contains(name));

            if (orderBy == "horizontalImage")
            {
                if (isAscOrder)
                    Query.OrderBy(c => c.SaleTranslations.FirstOrDefault(
                                  t => t.LanguageId == CurrentLanguage.Id).HorizontalImage);
                else
                    Query.OrderByDescending(c => c.SaleTranslations.FirstOrDefault(
                                            t => t.LanguageId == CurrentLanguage.Id).HorizontalImage);
            }
            if (orderBy == "verticalImage")
            {
                if (isAscOrder)
                    Query.OrderBy(c => c.SaleTranslations.FirstOrDefault(
                                  t => t.LanguageId == CurrentLanguage.Id).VerticalImage);
                else
                    Query.OrderByDescending(c => c.SaleTranslations.FirstOrDefault(
                                            t => t.LanguageId == CurrentLanguage.Id).VerticalImage);
            }

            if (isAscOrder)
                Query.OrderBy(orderBy);
            else
                Query.OrderByDescending(orderBy);

            if (skip.HasValue)
                Query.Skip(skip.Value);

            if (take.HasValue)
                Query.Take(take.Value);
        }
    }
}
