using Ardalis.Specification;
using DAL.Entities;
using WebAPI.Extensions;
using WebAPI.Helpers;

namespace WebAPI.Specifications.Categories
{
    public class CategorySearchSpecification : Specification<Category>
    {
        public CategorySearchSpecification(string name, bool isAscOrder, string orderBy, int? skip = null, int? take = null)
        {
            Query.Include(o => o.CategoryTranslations)
                 .Include(o => o.Parent).ThenInclude(p => p.CategoryTranslations);

            if (!string.IsNullOrEmpty(name))
                Query.Where(item => item.CategoryTranslations.FirstOrDefault(c => c.LanguageId == CurrentLanguage.Id).Name.Contains(name));


            if (orderBy == "name")
            {
                if (isAscOrder)
                    Query.OrderBy(c => c.CategoryTranslations.FirstOrDefault(c => c.LanguageId == CurrentLanguage.Id).Name);
                else
                    Query.OrderByDescending(c => c.CategoryTranslations.FirstOrDefault(c => c.LanguageId == CurrentLanguage.Id).Name);
            }
            else if (orderBy == "parentName")
            {
                if (isAscOrder)
                    Query.OrderBy(c => c.Parent.CategoryTranslations.FirstOrDefault(c => c.LanguageId == CurrentLanguage.Id).Name);
                else
                    Query.OrderByDescending(c => c.Parent.CategoryTranslations.FirstOrDefault(c => c.LanguageId == CurrentLanguage.Id).Name);
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
