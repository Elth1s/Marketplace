using WebAPI.ViewModels.Request;
using WebAPI.ViewModels.Response;

namespace WebAPI.Interfaces
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
