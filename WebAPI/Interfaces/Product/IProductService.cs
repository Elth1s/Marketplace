using WebAPI.ViewModels.Request;
using WebAPI.ViewModels.Response;

namespace WebAPI.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<ProductResponse>> GetAsync();
        Task<ProductResponse> GetByIdAsync(int id);
        Task CreateAsync(ProductCreateRequest request);
        //Task UpdateAsync(int id, ProductUpdateRequest request);
        Task DeleteAsync(int id);
    }
}
