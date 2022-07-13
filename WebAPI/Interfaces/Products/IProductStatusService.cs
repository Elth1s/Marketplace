using WebAPI.ViewModels.Request;
using WebAPI.ViewModels.Request.Products;
using WebAPI.ViewModels.Response;
using WebAPI.ViewModels.Response.Products;

namespace WebAPI.Interfaces.Products
{
    public interface IProductStatusService
    {
        Task<IEnumerable<ProductStatusResponse>> GetAsync();
        Task<AdminSearchResponse<ProductStatusResponse>> SearchProductStatusesAsync(AdminSearchRequest request);
        Task<ProductStatusResponse> GetByIdAsync(int id);
        Task CreateAsync(ProductStatusRequest request);
        Task UpdateAsync(int id, ProductStatusRequest request);
        Task DeleteAsync(int id);
        Task DeleteProductStatusesAsync(IEnumerable<int> ids);
    }
}
