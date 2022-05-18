using AutoMapper;
using DAL;
using DAL.Entities;
using WebAPI.Extensions;
using WebAPI.Interfaces;
using WebAPI.Specifications;
using WebAPI.ViewModels.Request;
using WebAPI.ViewModels.Response;

namespace WebAPI.Services
{
    public class CharacteristicService : ICharacteristicService
    {
        private readonly IRepository<Characteristic> _characteristicRepository;
        private readonly IMapper _mapper;

        public CharacteristicService(IRepository<Characteristic> countryRepository, IMapper mapper)
        {
            _characteristicRepository = countryRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CharacteristicResponse>> GetAsync()
        {
            var spec = new CharacteristicIncludeFullInfoSpecification();
            var characteristics = await _characteristicRepository.ListAsync(spec);

            return _mapper.Map<IEnumerable<CharacteristicResponse>>(characteristics);
        }

        public async Task<CharacteristicResponse> GetByIdAsync(int id)
        {
            var spec = new CharacteristicIncludeFullInfoSpecification(id);
            var characteristic = await _characteristicRepository.GetBySpecAsync(spec);
            characteristic.CharacteristicNullChecking();

            return _mapper.Map<CharacteristicResponse>(characteristic);
        }

        public async Task CreateAsync(CharacteristicRequest request)
        {
            var characteristic = _mapper.Map<Characteristic>(request);

            await _characteristicRepository.AddAsync(characteristic);
            await _characteristicRepository.SaveChangesAsync();
        }

        public async Task UpdateAsync(int id, CharacteristicRequest request)
        {
            var characteristic = await _characteristicRepository.GetByIdAsync(id);
            characteristic.CharacteristicNullChecking();

            _mapper.Map(request, characteristic);

            await _characteristicRepository.UpdateAsync(characteristic);
            await _characteristicRepository.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var characteristic = await _characteristicRepository.GetByIdAsync(id);
            characteristic.CharacteristicNullChecking();

            await _characteristicRepository.DeleteAsync(characteristic);
            await _characteristicRepository.SaveChangesAsync();
        }
    }
}
