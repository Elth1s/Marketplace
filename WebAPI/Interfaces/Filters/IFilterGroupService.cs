using WebAPI.ViewModels.Request;
using WebAPI.ViewModels.Request.Filters;
using WebAPI.ViewModels.Response;
using WebAPI.ViewModels.Response.Filters;

namespace WebAPI.Interfaces.Filters
{
    public interface IFilterGroupService
    {
        Task<IEnumerable<FilterGroupResponse>> GetFilterGroupsAsync();
        Task<AdminSearchResponse<FilterGroupResponse>> SearchAsync(AdminSearchRequest request);
        Task<FilterGroupResponse> GetFilterGroupByIdAsync(int filterGroupId);
        Task CreateFilterGroupAsync(FilterGroupRequest request);
        Task UpdateFilterGroupAsync(int filterGroupId, FilterGroupRequest request);
        Task DeleteFilterGroupAsync(int filterGroupId);
        Task DeleteAsync(IEnumerable<int> ids);
    }
}
