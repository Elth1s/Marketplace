using WebAPI.ViewModels.Request;
using WebAPI.ViewModels.Response;

namespace WebAPI.Interfaces
{
    public interface IFilterService
    {
        Task<IEnumerable<FilterResponse>> GetFiltersAsync();
        Task<FilterResponse> GetFilterByIdAsync(int filterId);
        Task CreateFilterAsync(FilterRequest request);
        Task UpdateFilterAsync(int filterId, FilterRequest request);
        Task DeleteFilterAsync(int filterId);
    }
}
