using WebAPI.ViewModels.Request;
using WebAPI.ViewModels.Response;
using WebAPI.ViewModels.Response.Cities;

namespace WebAPI.Interfaces
{
    public interface ICityService
    {
        Task<IEnumerable<CityResponse>> GetCitiesAsync();
        Task<SearchResponse<CityResponse>> SearchCitiesAsync(AdminSearchRequest request);
        Task<CityFullInfoResponse> GetCityByIdAsync(int cityId);
        Task CreateCityAsync(CityRequest request);
        Task UpdateCityAsync(int cityId, CityRequest request);
        Task DeleteCityAsync(int cityId);
        Task DeleteCitiesAsync(IEnumerable<int> ids);
    }
}
