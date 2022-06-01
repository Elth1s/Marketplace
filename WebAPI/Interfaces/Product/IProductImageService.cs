using WebAPI.ViewModels.Request;
using WebAPI.ViewModels.Response;

namespace WebAPI.Interfaces
{
    public interface IProductImageService
    {
        Task<IEnumerable<ProductImageResponse>> GetAsync();
        Task<ProductImageResponse> GetByIdAsync(int id);
        Task CreateAsync(ProductImageRequest request);
        Task UpdateAsync(int id, ProductImageRequest request);
        Task DeleteAsync(int id);
    }
}
