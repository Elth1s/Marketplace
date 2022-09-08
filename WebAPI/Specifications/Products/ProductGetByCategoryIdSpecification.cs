﻿using Ardalis.Specification;
using DAL.Entities;
using WebAPI.Helpers;

namespace WebAPI.Specifications.Products
{
    public class ProductGetByCategoryIdSpecification : Specification<Product>
    {
        public ProductGetByCategoryIdSpecification(int categoryId, List<FilterValue> filters, int? page, int? rowsPerPage, int? productId = null)
        {
            Query.Where(item => categoryId == item.CategoryId)
                 .Include(c => c.Category)
                 .ThenInclude(c => c.CategoryTranslations)
                 .Include(o => o.Status).ThenInclude(s => s.ProductStatusTranslations)
                 .Where(item => !item.IsDeleted);

            if (productId != null)
                Query.Where(item => productId != item.Id);

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

            Query.Include(p => p.Status)
                 .Include(p => p.Images)
                 .AsSplitQuery();

            if (page.HasValue && rowsPerPage.HasValue)
            {
                Query.Skip((page.Value - 1) * rowsPerPage.Value)
                     .Take(rowsPerPage.Value);
            }
        }

        public ProductGetByCategoryIdSpecification(string productName, int categoryId, int? shopId = null)
        {
            Query.Where(item => categoryId == item.CategoryId)
                 .Where(item => !item.IsDeleted);

            if (!string.IsNullOrEmpty(productName))
                Query.Where(item => item.Name.Contains(productName));

            if (shopId.HasValue)
                Query.Where(item => item.ShopId == shopId.Value);

        }
    }
}
