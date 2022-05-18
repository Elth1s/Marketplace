using WebAPI.ViewModels.Request;
using WebAPI.ViewModels.Response;

namespace WebAPI.Interfaces
{
    public interface ICharacteristicService
    {
        Task<IEnumerable<CharacteristicResponse>> GetAsync();
        Task<CharacteristicResponse> GetByIdAsync(int id);
        Task CreateAsync(CharacteristicRequest request);
        Task UpdateAsync(int id, CharacteristicRequest request);
        Task DeleteAsync(int id);
    }
}
