using WebAPI.ViewModels.Request.Filters;
using WebAPI.ViewModels.Response.Filters;

namespace WebAPI.Interfaces.Filters
{
    public interface IFilterNameService
    {
        Task<IEnumerable<FilterNameResponse>> GetFiltersNameAsync();
        Task<FilterNameResponse> GetFilterNameByIdAsync(int filterNameId);
        Task CreateFilterNameAsync(FilterNameRequest request);
        Task UpdateFilterNameAsync(int filterNameId, FilterNameRequest request);
        Task DeleteFilterNameAsync(int filterNameId);
    }
}
