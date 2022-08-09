using AutoMapper;
using DAL;
using DAL.Entities;
using Microsoft.Extensions.Localization;
using WebAPI.Exceptions;
using WebAPI.Extensions;
using WebAPI.Interfaces.Characteristics;
using WebAPI.Specifications.Characteristics;
using WebAPI.ViewModels.Request;
using WebAPI.ViewModels.Request.Characteristics;
using WebAPI.ViewModels.Response;
using WebAPI.ViewModels.Response.Characteristics;

namespace WebAPI.Services.Characteristcs
{
    public class CharacteristicNameService : ICharacteristicNameService
    {
        private readonly IStringLocalizer<ErrorMessages> _errorMessagesLocalizer;
        private readonly IRepository<CharacteristicName> _characteristicNameRepository;
        private readonly IRepository<CharacteristicGroup> _characteristicGroupRepository;
        private readonly IRepository<Unit> _unitRepository;
        private readonly IMapper _mapper;

        public CharacteristicNameService(IStringLocalizer<ErrorMessages> errorMessagesLocalizer,
            IRepository<CharacteristicName> characteristicNameRepository,
            IRepository<CharacteristicGroup> characteristicGroupRepository,
            IRepository<Unit> unitRepository,
            IMapper mapper)
        {
            _errorMessagesLocalizer = errorMessagesLocalizer;
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

        public async Task<AdminSearchResponse<CharacteristicNameResponse>> SearchAsync(AdminSearchRequest request)
        {
            var spec = new CharacteristicNameSearchSpecification(request.Name, request.IsAscOrder, request.OrderBy);
            var count = await _characteristicNameRepository.CountAsync(spec);
            spec = new CharacteristicNameSearchSpecification(
                request.Name,
                request.IsAscOrder,
                request.OrderBy,
                (request.Page - 1) * request.RowsPerPage,
                request.RowsPerPage);

            var characteristicNames = await _characteristicNameRepository.ListAsync(spec);
            var mappedCharacteristicNames = _mapper.Map<IEnumerable<CharacteristicNameResponse>>(characteristicNames);
            var response = new AdminSearchResponse<CharacteristicNameResponse>() { Count = count, Values = mappedCharacteristicNames };

            return response;
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
            {
                throw new AppValidationException(
                    new ValidationError(nameof(CharacteristicName.UnitId), _errorMessagesLocalizer["CharacteristicNameUnitNotUnique"]));
            }
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
                throw new AppValidationException(
                    new ValidationError(nameof(CharacteristicName.UnitId), _errorMessagesLocalizer["CharacteristicNameUnitNotUnique"]));

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

        public async Task DeleteAsync(IEnumerable<int> ids)
        {
            foreach (var item in ids)
            {
                var characteristicName = await _characteristicNameRepository.GetByIdAsync(item);
                await _characteristicNameRepository.DeleteAsync(characteristicName);
            }
            await _characteristicNameRepository.SaveChangesAsync();
        }
    }
}
