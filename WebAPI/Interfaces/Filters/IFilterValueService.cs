using WebAPI.ViewModels.Request;
using WebAPI.ViewModels.Request.Filters;
using WebAPI.ViewModels.Response;
using WebAPI.ViewModels.Response.Filters;

namespace WebAPI.Interfaces.Filters
{
    public interface IFilterValueService
    {
        Task<IEnumerable<FilterValueResponse>> GetFiltersValueAsync();
        Task<AdminSearchResponse<FilterValueResponse>> SearchAsync(AdminSearchRequest request);
        Task<FilterValueResponse> GetFilterValueByIdAsync(int filterValueId);
        Task CreateFilterValueAsync(FilterValueRequest request);
        Task UpdateFilterValueAsync(int filterValueId, FilterValueRequest request);
        Task DeleteFilterValueAsync(int filterValueId);
        Task DeleteAsync(IEnumerable<int> ids);
    }
}
