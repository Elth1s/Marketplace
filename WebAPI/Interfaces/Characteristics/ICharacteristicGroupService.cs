using WebAPI.ViewModels.Request.Characteristics;
using WebAPI.ViewModels.Response.Characteristics;

namespace WebAPI.Interfaces.Characteristics
{
    public interface ICharacteristicGroupService
    {
        Task<IEnumerable<CharacteristicGroupResponse>> GetAsync(string userId);
        Task<SearchCharacteristicGroupResponse> SearchCharacteristicGroupsAsync(SearchCharacteristicGroupRequest request);
        Task<CharacteristicGroupResponse> GetByIdAsync(int id);
        Task CreateAsync(CharacteristicGroupRequest request, string userId);
        Task UpdateAsync(int id, CharacteristicGroupRequest request);
        Task DeleteAsync(int id);
        Task DeleteAsync(IEnumerable<int> id);
    }
}
