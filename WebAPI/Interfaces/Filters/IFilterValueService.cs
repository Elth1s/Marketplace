using WebAPI.ViewModels.Request.Filters;
using WebAPI.ViewModels.Response.Filters;

namespace WebAPI.Interfaces.Filters
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
