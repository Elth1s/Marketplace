using AutoMapper;
using DAL;
using DAL.Entities;
using WebAPI.Exceptions;
using WebAPI.Extensions;
using WebAPI.Interfaces;
using WebAPI.Resources;
using WebAPI.Specifications;
using WebAPI.ViewModels.Request;
using WebAPI.ViewModels.Response;

namespace WebAPI.Services
{
    public class CharacteristicNameService : ICharacteristicNameService
    {
        private readonly IRepository<CharacteristicName> _characteristicNameRepository;
        private readonly IRepository<CharacteristicGroup> _characteristicGroupRepository;
        private readonly IRepository<Unit> _unitRepository;
        private readonly IMapper _mapper;

        public CharacteristicNameService(IRepository<CharacteristicName> characteristicNameRepository, IRepository<CharacteristicGroup> characteristicGroupRepository, IRepository<Unit> unitRepository, IMapper mapper)
        {
            _characteristicNameRepository = characteristicNameRepository;
            _characteristicGroupRepository = characteristicGroupRepository;
            _unitRepository = unitRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CharacteristicNameResponse>> GetAsync()
        {
            var spec = new CharacteristicNameIncludeFullInfoSpecification();
            var characteristicNames = await _characteristicNameRepository.ListAsync(spec);

            return _mapper.Map<IEnumerable<CharacteristicNameResponse>>(characteristicNames);
        }

        public async Task<CharacteristicNameResponse> GetByIdAsync(int id)
        {
            var spec = new CharacteristicNameIncludeFullInfoSpecification(id);
            var characteristicName = await _characteristicNameRepository.GetBySpecAsync(spec);
            characteristicName.CharacteristicNameNullChecking();

            return _mapper.Map<CharacteristicNameResponse>(characteristicName);
        }

        public async Task CreateAsync(CharacteristicNameRequest request)
        {
            var characteristicGroup = await _characteristicGroupRepository.GetByIdAsync(request.CharacteristicGroupId);
            characteristicGroup.CharacteristicGroupNullChecking();

            if (request.UnitId != null)
            {
                var unit = await _unitRepository.GetByIdAsync(request.UnitId);
                unit.UnitNullChecking();
            }

            var spec = new CharacteristicNameGetByNameAndUnitIdSpecification(request.Name, request.UnitId);
            if (await _characteristicNameRepository.GetBySpecAsync(spec) != null)
                throw new AppException(ErrorMessages.CharacteristicNameUnitNotUnique);

            var characteristicName = _mapper.Map<CharacteristicName>(request);

            await _characteristicNameRepository.AddAsync(characteristicName);
            await _characteristicNameRepository.SaveChangesAsync();
        }

        public async Task UpdateAsync(int id, CharacteristicNameRequest request)
        {
            var characteristicGroup = await _characteristicGroupRepository.GetByIdAsync(request.CharacteristicGroupId);
            characteristicGroup.CharacteristicGroupNullChecking();

            if (request.UnitId != null)
            {
                var unit = await _unitRepository.GetByIdAsync(request.UnitId);
                unit.UnitNullChecking();
            }

            var spec = new CharacteristicNameGetByNameAndUnitIdSpecification(request.Name, request.UnitId);
            if (await _characteristicNameRepository.GetBySpecAsync(spec) != null)
                throw new AppException(ErrorMessages.CharacteristicNameUnitNotUnique);

            var characteristicName = await _characteristicNameRepository.GetByIdAsync(id);
            characteristicName.CharacteristicNameNullChecking();

            _mapper.Map(request, characteristicName);

            await _characteristicNameRepository.UpdateAsync(characteristicName);
            await _characteristicNameRepository.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var characteristicName = await _characteristicNameRepository.GetByIdAsync(id);
            characteristicName.CharacteristicNameNullChecking();

            await _characteristicNameRepository.DeleteAsync(characteristicName);
            await _characteristicNameRepository.SaveChangesAsync();
        }
    }
}
