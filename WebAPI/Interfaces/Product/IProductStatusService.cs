using WebAPI.ViewModels.Request;
using WebAPI.ViewModels.Response;

namespace WebAPI.Interfaces
{
    public interface IProductStatusService
    {
        Task<IEnumerable<ProductStatusResponse>> GetAsync();
        Task<ProductStatusResponse> GetByIdAsync(int id);
        Task CreateAsync(ProductStatusRequest request);
        Task UpdateAsync(int id, ProductStatusRequest request);
        Task DeleteAsync(int id);
    }
}
