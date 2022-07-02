using WebAPI.ViewModels.Request;
using WebAPI.ViewModels.Request.Characteristics;
using WebAPI.ViewModels.Response;
using WebAPI.ViewModels.Response.Characteristics;

namespace WebAPI.Interfaces.Characteristics
{
    public interface ICharacteristicGroupService
    {
        Task<IEnumerable<CharacteristicGroupResponse>> GetAsync(string userId);
        Task<AdminSearchResponse<CharacteristicGroupResponse>> SearchCharacteristicGroupsAsync(AdminSearchRequest request);
        Task<CharacteristicGroupResponse> GetByIdAsync(int id);
        Task CreateAsync(CharacteristicGroupRequest request, string userId);
        Task UpdateAsync(int id, CharacteristicGroupRequest request);
        Task DeleteAsync(int id);
        Task DeleteAsync(IEnumerable<int> id);
    }
}
