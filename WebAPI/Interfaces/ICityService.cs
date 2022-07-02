using WebAPI.ViewModels.Request;
using WebAPI.ViewModels.Response;

namespace WebAPI.Interfaces
{
    public interface ICityService
    {
        Task<IEnumerable<CityResponse>> GetCitiesAsync();
        Task<AdminSearchResponse<CityResponse>> SearchCitiesAsync(AdminSearchRequest request);
        Task<CityResponse> GetCityByIdAsync(int cityId);
        Task CreateCityAsync(CityRequest request);
        Task UpdateCityAsync(int cityId, CityRequest request);
        Task DeleteCityAsync(int cityId);
        Task DeleteCitiesAsync(IEnumerable<int> ids);
    }
}
