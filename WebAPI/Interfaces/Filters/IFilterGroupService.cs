using WebAPI.ViewModels.Request.Filters;
using WebAPI.ViewModels.Response.Filters;

namespace WebAPI.Interfaces.Filters
{
    public interface IFilterGroupService
    {
        Task<IEnumerable<FilterGroupResponse>> GetFilterGroupsAsync();
        Task<FilterGroupResponse> GetFilterGroupByIdAsync(int filterGroupId);
        Task CreateFilterGroupAsync(FilterGroupRequest request);
        Task UpdateFilterGroupAsync(int filterGroupId, FilterGroupRequest request);
        Task DeleteFilterGroupAsync(int filterGroupId);
    }
}
