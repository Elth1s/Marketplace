using WebAPI.ViewModels.Request;
using WebAPI.ViewModels.Request.Products;
using WebAPI.ViewModels.Response;
using WebAPI.ViewModels.Response.Products;

namespace WebAPI.Interfaces.Products
{
    public interface IProductService
    {
        Task<IEnumerable<ProductResponse>> GetAsync();
        Task<SearchResponse<ProductResponse>> AdminSellerSearchProductsAsync(SellerSearchRequest request, string userId);
        Task<SearchResponse<ProductCatalogResponse>> SearchProductsAsync(SearchProductRequest request, string userId);
        Task<ProductResponse> GetByIdAsync(int id);
        Task<ProductRatingResponse> GetProductRatingByUrlSlugAsync(string urlSlug);
        Task<ProductWithCategoryParentsResponse> GetByUrlSlugAsync(string urlSlug, string userId);
        Task<IEnumerable<ProductCatalogResponse>> GetSimilarProductsAsync(string urlSlug, string userId);
        Task CreateAsync(ProductCreateRequest request, string userId);
        //Task UpdateAsync(int id, ProductUpdateRequest request);
        Task DeleteAsync(int id);
        Task DeleteProductsAsync(IEnumerable<int> ids);

        Task ChangeSelectProductAsync(string productSlug, string userId);
        Task<IEnumerable<ProductWithCartResponse>> GetSelectedProductsAsync(string userId);
        Task<IEnumerable<ProductWithCartResponse>> GetReviewedProductsAsync(string userId);
    }
}
