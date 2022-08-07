using WebAPI.ViewModels.Request;
using WebAPI.ViewModels.Request.Products;
using WebAPI.ViewModels.Response;
using WebAPI.ViewModels.Response.Products;

namespace WebAPI.Interfaces.Products
{
    public interface IProductService
    {
        Task<IEnumerable<ProductResponse>> GetAsync();
        Task<AdminSearchResponse<ProductResponse>> SearchProductsAsync(SellerSearchRequest request, string userId);
        Task<ProductResponse> GetByIdAsync(int id);
        Task<ProductWithCategoryParentsResponse> GetByUrlSlugAsync(string urlSlug, string userId);
        Task<IEnumerable<ProductCatalogResponse>> GetSimilarProductsAsync(string urlSlug);
        Task CreateAsync(ProductCreateRequest request, string userId);
        //Task UpdateAsync(int id, ProductUpdateRequest request);
        Task DeleteAsync(int id);
        Task DeleteProductsAsync(IEnumerable<int> ids);
    }
}
