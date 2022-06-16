using WebAPI.ViewModels.Request;
using WebAPI.ViewModels.Response;

namespace WebAPI.Interfaces
{
    public interface IFilterValueService
    {
        Task<IEnumerable<FilterValueResponse>> GetFiltersValueAsync();
        Task<FilterValueResponse> GetFilterValueByIdAsync(int filterValueId);
        Task CreateFilterValueAsync(FilterValueRequest request);
        Task UpdateFilterValueAsync(int filterValueId, FilterValueRequest request);
        Task DeleteFilterValueAsync(int filterValueId);
    }
}
