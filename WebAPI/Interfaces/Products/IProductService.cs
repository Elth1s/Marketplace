﻿using WebAPI.ViewModels.Request;
using WebAPI.ViewModels.Request.Products;
using WebAPI.ViewModels.Response;
using WebAPI.ViewModels.Response.Products;

namespace WebAPI.Interfaces.Products
{
    public interface IProductService
    {
        Task<IEnumerable<ProductResponse>> GetAsync();
        Task<SearchResponse<ProductResponse>> AdminSellerSearchProductsAsync(SellerSearchRequest request, string userId);
        Task<SearchResponse<ProductCatalogResponse>> SearchProductsAsync(SearchProductsRequest request, string userId);
        Task<SearchResponse<ProductCatalogResponse>> GetProductsBySaleAsync(SaleProductsRequest request, string userId);
        Task<SearchResponse<ProductCatalogResponse>> GetNoveltiesAsync(NoveltyProductsRequest request, string userId);
        Task<ProductResponse> GetByIdAsync(int id);
        Task<ProductRatingResponse> GetProductRatingByUrlSlugAsync(string urlSlug);
        Task<ProductWithCategoryParentsResponse> GetByUrlSlugAsync(string urlSlug, string userId);
        Task<IEnumerable<ProductCatalogResponse>> GetSimilarProductsAsync(string urlSlug, string userId);
        Task CreateAsync(ProductCreateRequest request, string userId);
        Task UpdateDiscountAsync(int id, ProductDiscountRequest request, string userId);
        Task DeleteAsync(int id);
        Task DeleteProductsAsync(IEnumerable<int> ids);

        Task ChangeSelectProductAsync(string productSlug, string userId);
        Task ChangeComparisonProductAsync(string productSlug, string userId);
        Task<IEnumerable<ProductWithCartResponse>> GetSelectedProductsAsync(string userId);
        Task<IEnumerable<ProductWithCartResponse>> GetReviewedProductsAsync(string userId);
        Task<ComparisonResponse> GetComparisonProductsAsync(string categorySlug, string userId);

        Task<IEnumerable<ComparisonItemResponse>> GetComparisonAsync(string userId);
    }
}
