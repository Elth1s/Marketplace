using Ardalis.Specification;
using DAL.Entities;
using WebAPI.Extensions;
using WebAPI.Helpers;

namespace WebAPI.Specifications.Orders
{
    public class OrderStatusSearchSpecification : Specification<OrderStatus>
    {
        public OrderStatusSearchSpecification(string name, bool isAscOrder, string orderBy, int? skip = null, int? take = null)
        {
            Query.Include(c => c.OrderStatusTranslations);

            if (!string.IsNullOrEmpty(name))
                Query.Where(item => item.OrderStatusTranslations.Any(c => c.LanguageId == CurrentLanguage.Id && c.Name.Contains(name)));

            if (orderBy == "name")
            {
                if (isAscOrder)
                    Query.OrderBy(c => c.OrderStatusTranslations.FirstOrDefault(t => t.LanguageId == CurrentLanguage.Id).Name);
                else
                    Query.OrderByDescending(c => c.OrderStatusTranslations.FirstOrDefault(t => t.LanguageId == CurrentLanguage.Id).Name);
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
