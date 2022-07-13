using WebAPI.ViewModels.Request;
using WebAPI.ViewModels.Request.Characteristics;
using WebAPI.ViewModels.Response;
using WebAPI.ViewModels.Response.Characteristics;

namespace WebAPI.Interfaces.Characteristics
{
    public interface ICharacteristicValueService
    {
        Task<IEnumerable<CharacteristicValueResponse>> GetAsync();
        Task<AdminSearchResponse<CharacteristicValueResponse>> SearchAsync(AdminSearchRequest request);
        Task<CharacteristicValueResponse> GetByIdAsync(int id);
        Task CreateAsync(CharacteristicValueRequest request);
        Task UpdateAsync(int id, CharacteristicValueRequest request);
        Task DeleteAsync(int id);
        Task DeleteAsync(IEnumerable<int> ids);
    }
}
