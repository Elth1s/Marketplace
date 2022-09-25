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
            Query.Where(item => !item.IsDeleted)
                 .Include(o => o.Category).ThenInclude(c => c.CategoryTranslations)
                 .Include(o => o.Status).ThenInclude(s => s.ProductStatusTranslations)
                 .Include(pi => pi.Images).AsSplitQuery();

            if (!string.IsNullOrEmpty(name))
                if (shopId != null)
                    Query.Where(item => item.Name.Contains(name));

            if (isSeller)
                Query.Where(item => item.ShopId == shopId.Value);

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

        public ProductSearchSpecification(int? shopId, string productName, List<Category> categories, List<FilterValue> filters, int? page, int? rowsPerPage, int? min = null, int? max = null)
        {
            Query.Where(item => !item.IsDeleted);

            if (!string.IsNullOrEmpty(productName))
                Query.Where(item => item.Name.Contains(productName));

            if (shopId != null)
                Query.Where(item => item.ShopId == shopId);

            Query.Include(o => o.Category).ThenInclude(c => c.CategoryTranslations)
                 .Include(o => o.Status).ThenInclude(s => s.ProductStatusTranslations)
                 .Include(pi => pi.Images);



            if (categories != null)
            {
                var productPredicate = PredicateBuilder.False<Product>();
                foreach (var category in categories)
                {
                    productPredicate = productPredicate
                        .Or(p => p.Category.Id == category.Id);
                }
                Query.Where(productPredicate);
            }

            if (filters != null)
            {
                var gropedFilters = filters.GroupBy(f => f.FilterNameId);

                foreach (var item in gropedFilters)
                {
                    var productPredicate = PredicateBuilder.False<Product>();
                    foreach (var filterValue in item)
                    {
                        productPredicate = productPredicate
                            .Or(p => p.FilterValueProducts
                                    .Any(f => f.FilterValueId == filterValue.Id));
                    }
                    Query.Where(productPredicate);
                }
            }
            if (min.HasValue && max.HasValue)
                Query.Where(p => (p.Discount > 0 ? p.Price - (p.Price / 100f * p.Discount) : p.Price) >= min.Value
                && (p.Discount > 0 ? p.Price - (p.Price / 100f * p.Discount) : p.Price) <= max.Value);

            if (page.HasValue && rowsPerPage.HasValue)
            {
                Query.Skip((page.Value - 1) * rowsPerPage.Value)
                     .Take(rowsPerPage.Value);
            }
        }
        public ProductSearchSpecification(int? shopId, string productName, List<Category> categories, List<FilterValue> filters, int? min = null, int? max = null)
        {
            Query.Where(item => !item.IsDeleted);

            if (!string.IsNullOrEmpty(productName))
                Query.Where(item => item.Name.Contains(productName));

            if (shopId != null)
                Query.Where(item => item.ShopId == shopId);
            if (categories != null)
            {
                var productPredicate = PredicateBuilder.False<Product>();
                foreach (var category in categories)
                {
                    productPredicate = productPredicate
                        .Or(p => p.Category.Id == category.Id);
                }
                Query.Where(productPredicate);
            }

            if (filters != null)
            {
                var gropedFilters = filters.GroupBy(f => f.FilterNameId);

                foreach (var item in gropedFilters)
                {
                    var productPredicate = PredicateBuilder.False<Product>();
                    foreach (var filterValue in item)
                    {
                        productPredicate = productPredicate
                            .Or(p => p.FilterValueProducts
                                    .Any(f => f.FilterValueId == filterValue.Id));
                    }
                    Query.Where(productPredicate);
                }
            }

            if (min.HasValue && max.HasValue)
                Query.Where(p => (p.Discount > 0 ? p.Price - (p.Price / 100f * p.Discount) : p.Price) >= min.Value
                && (p.Discount > 0 ? p.Price - (p.Price / 100f * p.Discount) : p.Price) <= max.Value);

            Query.OrderBy(p => p.Discount > 0 ? p.Price - (p.Price / 100f * p.Discount) : p.Price);

            Query.PostProcessingAction(p => new List<Product>() { p.FirstOrDefault(), p.LastOrDefault() });
        }
        public ProductSearchSpecification(List<Category> categories, List<FilterValue> filters, int? page, int? rowsPerPage)
        {
            Query.Include(o => o.Category).ThenInclude(c => c.CategoryTranslations)
                 .Include(o => o.Status).ThenInclude(s => s.ProductStatusTranslations)
                 .Include(pi => pi.Images)
                 .OrderByDescending(p => p.Id)
                 .Where(item => !item.IsDeleted);

            if (categories != null)
            {
                var productPredicate = PredicateBuilder.False<Product>();
                foreach (var category in categories)
                {
                    productPredicate = productPredicate
                        .Or(p => p.Category.Id == category.Id);
                }
                Query.Where(productPredicate);
            }

            if (filters != null)
            {
                var gropedFilters = filters.GroupBy(f => f.FilterNameId);

                foreach (var item in gropedFilters)
                {
                    var productPredicate = PredicateBuilder.False<Product>();
                    foreach (var filterValue in item)
                    {
                        productPredicate = productPredicate
                            .Or(p => p.FilterValueProducts
                                    .Any(f => f.FilterValueId == filterValue.Id));
                    }
                    Query.Where(productPredicate);
                }
            }

            if (page.HasValue && rowsPerPage.HasValue)
            {
                Query.Skip((page.Value - 1) * rowsPerPage.Value)
                     .Take(rowsPerPage.Value);
            }
        }

        public ProductSearchSpecification(int? shopId, string productName)
        {
            if (!string.IsNullOrEmpty(productName))
                Query.Where(item => item.Name.Contains(productName));

            if (shopId != null)
                Query.Where(item => item.ShopId == shopId);

            Query.Include(o => o.Category).ThenInclude(c => c.CategoryTranslations)
                 .Include(o => o.Status).ThenInclude(s => s.ProductStatusTranslations)
                 .Include(pi => pi.Images);

        }

        public ProductSearchSpecification(int saleId, List<Category> categories, List<FilterValue> filters, int? page, int? rowsPerPage, int? min = null, int? max = null)
        {
            Query.Where(item => !item.IsDeleted)
                 .Where(item => item.SaleId == saleId);

            Query.Include(o => o.Category).ThenInclude(c => c.CategoryTranslations)
                 .Include(o => o.Status).ThenInclude(s => s.ProductStatusTranslations)
                 .Include(pi => pi.Images);

            if (categories != null)
            {
                var productPredicate = PredicateBuilder.False<Product>();
                foreach (var category in categories)
                {
                    productPredicate = productPredicate
                        .Or(p => p.Category.Id == category.Id);
                }
                Query.Where(productPredicate);
            }

            if (filters != null)
            {
                var gropedFilters = filters.GroupBy(f => f.FilterNameId);

                foreach (var item in gropedFilters)
                {
                    var productPredicate = PredicateBuilder.False<Product>();
                    foreach (var filterValue in item)
                    {
                        productPredicate = productPredicate
                            .Or(p => p.FilterValueProducts
                                    .Any(f => f.FilterValueId == filterValue.Id));
                    }
                    Query.Where(productPredicate);
                }
            }
            if (min.HasValue && max.HasValue)
                Query.Where(p => (p.Discount > 0 ? p.Price - (p.Price / 100f * p.Discount) : p.Price) >= min.Value
                && (p.Discount > 0 ? p.Price - (p.Price / 100f * p.Discount) : p.Price) <= max.Value);

            if (page.HasValue && rowsPerPage.HasValue)
            {
                Query.Skip((page.Value - 1) * rowsPerPage.Value)
                     .Take(rowsPerPage.Value);
            }
        }
        public ProductSearchSpecification(int saleId, List<Category> categories, List<FilterValue> filters, int? min = null, int? max = null)
        {
            Query.Where(item => !item.IsDeleted)
                .Where(item => item.SaleId == saleId);

            if (categories != null)
            {
                var productPredicate = PredicateBuilder.False<Product>();
                foreach (var category in categories)
                {
                    productPredicate = productPredicate
                        .Or(p => p.Category.Id == category.Id);
                }
                Query.Where(productPredicate);
            }

            if (filters != null)
            {
                var gropedFilters = filters.GroupBy(f => f.FilterNameId);

                foreach (var item in gropedFilters)
                {
                    var productPredicate = PredicateBuilder.False<Product>();
                    foreach (var filterValue in item)
                    {
                        productPredicate = productPredicate
                            .Or(p => p.FilterValueProducts
                                    .Any(f => f.FilterValueId == filterValue.Id));
                    }
                    Query.Where(productPredicate);
                }
            }

            if (min.HasValue && max.HasValue)
                Query.Where(p => (p.Discount > 0 ? p.Price - (p.Price / 100f * p.Discount) : p.Price) >= min.Value
                && (p.Discount > 0 ? p.Price - (p.Price / 100f * p.Discount) : p.Price) <= max.Value);

            Query.OrderBy(p => p.Discount > 0 ? p.Price - (p.Price / 100f * p.Discount) : p.Price);

            Query.PostProcessingAction(p => new List<Product>() { p.FirstOrDefault(), p.LastOrDefault() });
        }
    }
}
