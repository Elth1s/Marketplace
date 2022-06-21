using WebAPI.ViewModels.Request;
using WebAPI.ViewModels.Response;

namespace WebAPI.Interfaces
{
    public interface IUnitService
    {
        Task<IEnumerable<UnitResponse>> GetAllAsync();
        Task<UnitResponse> GetByIdAsync(int id);
        Task CreateAsync(UnitRequest request);
        Task UpdateAsync(int id, UnitRequest request);
        Task DeleteAsync(int id);
    }
}

