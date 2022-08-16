using WebAPI.ViewModels.Request;
using WebAPI.ViewModels.Response;
using WebAPI.ViewModels.Response.Units;

namespace WebAPI.Interfaces
{
    public interface IUnitService
    {
        Task<IEnumerable<UnitResponse>> GetAllAsync();
        Task<AdminSearchResponse<UnitResponse>> SearchUnitsAsync(AdminSearchRequest request);
        Task<UnitFullInfoResponse> GetByIdAsync(int id);
        Task CreateAsync(UnitRequest request);
        Task UpdateAsync(int id, UnitRequest request);
        Task DeleteAsync(int id);
        Task DeleteUnitsAsync(IEnumerable<int> ids);
    }
}

