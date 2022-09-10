using AutoMapper;
using DAL;
using DAL.Constants;
using DAL.Entities;
using WebAPI.Extensions;
using WebAPI.Interfaces;
using WebAPI.Specifications.Cities;
using WebAPI.ViewModels.Request;
using WebAPI.ViewModels.Response;
using WebAPI.ViewModels.Response.Cities;

namespace WebAPI.Services
{
    public class CityService : ICityService
    {
        private readonly IRepository<City> _cityRepository;
        private readonly IRepository<Country> _countryRepository;
        private readonly IMapper _mapper;

        public CityService(
            IRepository<City> cityRepository,
            IRepository<Country> countryRepository,
            IMapper mapper
            )
        {
            _cityRepository = cityRepository;
            _countryRepository = countryRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CityResponse>> GetCitiesAsync()
        {
            var spec = new CityIncludeFullInfoSpecification();
            var cities = await _cityRepository.ListAsync(spec);

            var response = _mapper.Map<IEnumerable<CityResponse>>(cities);
            return response;
        }

        public async Task<SearchResponse<CityResponse>> SearchCitiesAsync(AdminSearchRequest request)
        {
            var spec = new CitySearchSpecification(request.Name, request.IsAscOrder, request.OrderBy);
            var count = await _cityRepository.CountAsync(spec);
            spec = new CitySearchSpecification(
                request.Name,
                request.IsAscOrder,
                request.OrderBy,
                (request.Page - 1) * request.RowsPerPage,
                request.RowsPerPage);
            var cities = await _cityRepository.ListAsync(spec);
            var mappedCities = _mapper.Map<IEnumerable<CityResponse>>(cities);
            var response = new SearchResponse<CityResponse>() { Count = count, Values = mappedCities };

            return response;
        }

        public async Task<CityFullInfoResponse> GetCityByIdAsync(int cityId)
        {
            var spec = new CityIncludeFullInfoSpecification(cityId);
            var city = await _cityRepository.GetBySpecAsync(spec);
            city.CityNullChecking();

            var response = _mapper.Map<CityFullInfoResponse>(city);
            return response;
        }

        public async Task CreateCityAsync(CityRequest request)
        {
            var country = await _countryRepository.GetByIdAsync(request.CountryId);
            country.CountryNullChecking();

            var specName = new CityGetByNameAndCountryIdSpecification(request.EnglishName, request.CountryId, LanguageId.English);
            var cityEnNameExist = await _cityRepository.GetBySpecAsync(specName);
            if (cityEnNameExist != null)
                cityEnNameExist.CityWithEnglishNameChecking(nameof(CityRequest.EnglishName));

            specName = new CityGetByNameAndCountryIdSpecification(request.UkrainianName, request.CountryId, LanguageId.Ukrainian);
            var cityUkNameExist = await _cityRepository.GetBySpecAsync(specName);
            if (cityUkNameExist != null)
                cityUkNameExist.CityWithUkrainianNameChecking(nameof(CityRequest.UkrainianName));

            var city = _mapper.Map<City>(request);

            await _cityRepository.AddAsync(city);
            await _cityRepository.SaveChangesAsync();
        }

        public async Task UpdateCityAsync(int cityId, CityRequest request)
        {
            var spec = new CityIncludeFullInfoSpecification(cityId);
            var city = await _cityRepository.GetBySpecAsync(spec);
            city.CityNullChecking();

            var country = await _countryRepository.GetByIdAsync(request.CountryId);
            country.CountryNullChecking();


            var specName = new CityGetByNameAndCountryIdSpecification(request.EnglishName, request.CountryId, LanguageId.English);
            var cityEnNameExist = await _cityRepository.GetBySpecAsync(specName);
            if (cityEnNameExist != null && cityEnNameExist.Id != city.Id)
                cityEnNameExist.CityWithEnglishNameChecking(nameof(CityRequest.EnglishName));

            specName = new CityGetByNameAndCountryIdSpecification(request.UkrainianName, request.CountryId, LanguageId.Ukrainian);
            var cityUkNameExist = await _cityRepository.GetBySpecAsync(specName);
            if (cityUkNameExist != null && cityUkNameExist.Id != city.Id)
                cityUkNameExist.CityWithUkrainianNameChecking(nameof(CityRequest.UkrainianName));

            city.CityTranslations.Clear();

            _mapper.Map(request, city);

            await _cityRepository.UpdateAsync(city);
            await _cityRepository.SaveChangesAsync();
        }

        public async Task DeleteCityAsync(int cityId)
        {
            var city = await _cityRepository.GetByIdAsync(cityId);
            city.CityNullChecking();

            await _cityRepository.DeleteAsync(city);
            await _cityRepository.SaveChangesAsync();
        }

        public async Task DeleteCitiesAsync(IEnumerable<int> ids)
        {
            foreach (var item in ids)
            {
                var city = await _cityRepository.GetByIdAsync(item);
                //city.CityNullChecking();
                await _cityRepository.DeleteAsync(city);
            }
            await _cityRepository.SaveChangesAsync();
        }

        public async Task<IEnumerable<CityForSelectResponse>> GetCitiesByCountryAsync(int countryId)
        {
            var country = await _countryRepository.GetByIdAsync(countryId);
            country.CountryNullChecking();

            var spec = new CityGetByCountrySpecification(countryId);
            var cities = await _cityRepository.ListAsync(spec);

            var response = _mapper.Map<IEnumerable<CityForSelectResponse>>(cities);
            return response;
        }
    }
}
