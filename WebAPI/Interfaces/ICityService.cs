using WebAPI.ViewModels.Request;
using WebAPI.ViewModels.Response;

namespace WebAPI.Interfaces
{
    public interface ICityService
    {
        Task<IEnumerable<CityResponse>> GetCitiesAsync();
        Task<CityResponse> GetCityByIdAsync(int cityId);
        Task CreateCityAsync(CityRequest request);
        Task UpdateCityAsync(int cityId, CityRequest request);
        Task DeleteCityAsync(int cityId);
    }
}
