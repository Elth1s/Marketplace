using WebAPI.ViewModels.Request;
using WebAPI.ViewModels.Response;

namespace WebAPI.Interfaces
{
    public interface ICharacteristicGroupService
    {
        Task<IEnumerable<CharacteristicGroupResponse>> GetAsync();
        Task<CharacteristicGroupResponse> GetByIdAsync(int id);
        Task CreateAsync(CharacteristicGroupRequest request);
        Task UpdateAsync(int id, CharacteristicGroupRequest request);
        Task DeleteAsync(int id);
    }
}
