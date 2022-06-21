﻿using AutoMapper;
using DAL;
using DAL.Entities;
using WebAPI.Extensions;
using WebAPI.Interfaces;
using WebAPI.Resources;
using WebAPI.Specifications;
using WebAPI.ViewModels.Request;
using WebAPI.ViewModels.Response;

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

            var response = cities.Select(c => _mapper.Map<CityResponse>(c));
            return response;
        }
        public async Task<CityResponse> GetCityByIdAsync(int cityId)
        {
            var spec = new CityIncludeFullInfoSpecification(cityId);
            var city = await _cityRepository.GetBySpecAsync(spec);
            city.CityNullChecking();

            var response = _mapper.Map<CityResponse>(city);
            return response;
        }

        public async Task CreateCityAsync(CityRequest request)
        {
            var country = await _countryRepository.GetByIdAsync(request.CountryId);
            country.CountryNullChecking();

            var spec = new CityGetByNameAndCountryIdSpecification(request.Name, request.CountryId);
            if (await _cityRepository.GetBySpecAsync(spec) != null)
                throw new ApplicationException(ErrorMessages.CityCountryNotUnique);

            var city = _mapper.Map<City>(request);

            await _cityRepository.AddAsync(city);
            await _cityRepository.SaveChangesAsync();
        }

        public async Task UpdateCityAsync(int cityId, CityRequest request)
        {
            var country = await _countryRepository.GetByIdAsync(request.CountryId);
            country.CountryNullChecking();

            var city = await _cityRepository.GetByIdAsync(cityId);
            city.CityNullChecking();

            var spec = new CityGetByNameAndCountryIdSpecification(request.Name, request.CountryId);
            if (await _cityRepository.GetBySpecAsync(spec) != null)
                throw new ApplicationException(ErrorMessages.CityCountryNotUnique);

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
    }
}
