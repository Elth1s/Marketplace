using WebAPI.ViewModels.Request;
using WebAPI.ViewModels.Response;

namespace WebAPI.Interfaces
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
