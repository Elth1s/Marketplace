using WebAPI.ViewModels.Request;
using WebAPI.ViewModels.Request.Filters;
using WebAPI.ViewModels.Response;
using WebAPI.ViewModels.Response.Filters;

namespace WebAPI.Interfaces.Filters
{
    public interface IFilterNameService
    {
        Task<IEnumerable<FilterNameResponse>> GetFiltersNameAsync();
        Task<SearchResponse<FilterNameResponse>> SearchAsync(AdminSearchRequest request);
        Task<FilterNameFullInfoResponse> GetFilterNameByIdAsync(int filterNameId);
        Task CreateFilterNameAsync(FilterNameRequest request);
        Task UpdateFilterNameAsync(int filterNameId, FilterNameRequest request);
        Task DeleteFilterNameAsync(int filterNameId);
        Task DeleteAsync(IEnumerable<int> ids);
    }
}
