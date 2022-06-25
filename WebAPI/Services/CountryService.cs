using AutoMapper;
using DAL;
using DAL.Entities;
using WebAPI.Extensions;
using WebAPI.Interfaces;
using WebAPI.ViewModels.Request;
using WebAPI.ViewModels.Response;

namespace WebAPI.Services
{
    public class CountryService : ICountryService
    {
        private readonly IRepository<Country> _countryRepository;
        private readonly IMapper _mapper;

        public CountryService(
            IRepository<Country> countryRepository,
            IMapper mapper
            )
        {
            _countryRepository = countryRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<UnitResponse>> GetCountriesAsync()
        {
            var countries = await _countryRepository.ListAsync();

            var response = countries.Select(c => _mapper.Map<UnitResponse>(c));
            return response;
        }
        public async Task<UnitResponse> GetCountryByIdAsync(int countryId)
        {
            var country = await _countryRepository.GetByIdAsync(countryId);
            country.CountryNullChecking();

            var response = _mapper.Map<UnitResponse>(country);
            return response;
        }

        public async Task CreateCountryAsync(UnitRequest request)
        {
            var country = _mapper.Map<Country>(request);

            await _countryRepository.AddAsync(country);
            await _countryRepository.SaveChangesAsync();
        }

        public async Task UpdateCountryAsync(int countryId, UnitRequest request)
        {
            var country = await _countryRepository.GetByIdAsync(countryId);
            country.CountryNullChecking();

            _mapper.Map(request, country);

            await _countryRepository.UpdateAsync(country);
            await _countryRepository.SaveChangesAsync();
        }

        public async Task DeleteCountryAsync(int countryId)
        {
            var country = await _countryRepository.GetByIdAsync(countryId);
            country.CountryNullChecking();

            await _countryRepository.DeleteAsync(country);
            await _countryRepository.SaveChangesAsync();
        }
    }
}
