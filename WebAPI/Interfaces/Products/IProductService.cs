using WebAPI.ViewModels.Request;
using WebAPI.ViewModels.Request.Products;
using WebAPI.ViewModels.Response;
using WebAPI.ViewModels.Response.Products;

namespace WebAPI.Interfaces.Products
{
    public interface IProductService
    {
        Task<IEnumerable<ProductResponse>> GetAsync();
        Task<AdminSearchResponse<ProductResponse>> SearchProductsAsync(AdminSearchRequest request);
        Task<ProductResponse> GetByIdAsync(int id);
        Task<ProductWithCategoryParentsResponse> GetByUrlSlugAsync(string urlSlug, string userId);
        Task CreateAsync(ProductCreateRequest request);
        //Task UpdateAsync(int id, ProductUpdateRequest request);
        Task DeleteAsync(int id);
        Task DeleteProductsAsync(IEnumerable<int> ids);
    }
}
