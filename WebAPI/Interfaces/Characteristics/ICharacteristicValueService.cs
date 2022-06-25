using WebAPI.ViewModels.Request.Characteristics;
using WebAPI.ViewModels.Response.Characteristics;

namespace WebAPI.Interfaces.Characteristics
{
    public interface ICharacteristicValueService
    {
        Task<IEnumerable<CharacteristicValueResponse>> GetAsync();
        Task<CharacteristicValueResponse> GetByIdAsync(int id);
        Task CreateAsync(CharacteristicValueRequest request);
        Task UpdateAsync(int id, CharacteristicValueRequest request);
        Task DeleteAsync(int id);
    }
}
