using WebAPI.ViewModels.Request;
using WebAPI.ViewModels.Response;

namespace WebAPI.Interfaces
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
