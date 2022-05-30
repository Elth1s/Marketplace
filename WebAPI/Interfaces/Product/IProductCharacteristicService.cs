using WebAPI.ViewModels.Request;
using WebAPI.ViewModels.Response;

namespace WebAPI.Interfaces
{
    public interface IProductCharacteristicService
    {
        Task<IEnumerable<ProductCharacteristicResponse>> GetAsync();
        Task<ProductCharacteristicResponse> GetByIdAsync(int id);
        Task CreateAsync(ProductCharacteristicRequest request);
        Task UpdateAsync(int id, ProductCharacteristicRequest request);
        Task DeleteAsync(int id);
    }
}
