using AutoMapper;
using DAL;
using DAL.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using System.Net;
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
    public class CharacteristicValueService : ICharacteristicValueService
    {
        private readonly IStringLocalizer<ErrorMessages> _errorMessagesLocalizer;
        private readonly IRepository<CharacteristicValue> _characteristicValueRepository;
        private readonly IRepository<CharacteristicName> _characteristicNameRepository;
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;

        public CharacteristicValueService(IStringLocalizer<ErrorMessages> errorMessagesLocalizer,
        IRepository<CharacteristicValue> characteristicValueRepository,
            IRepository<CharacteristicName> characteristicNameRepository,
            UserManager<AppUser> userManager,
            IMapper mapper)
        {
            _errorMessagesLocalizer = errorMessagesLocalizer;
            _characteristicValueRepository = characteristicValueRepository;
            _characteristicNameRepository = characteristicNameRepository;
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CharacteristicValueResponse>> GetAsync()
        {
            var spec = new CharacteristicValueIncludeFullInfoSpecification();
            var characteristicValues = await _characteristicValueRepository.ListAsync(spec);

            return _mapper.Map<IEnumerable<CharacteristicValueResponse>>(characteristicValues);
        }

        public async Task<CharacteristicValueResponse> GetByIdAsync(int id)
        {
            var spec = new CharacteristicValueIncludeFullInfoSpecification(id);
            var characteristicValue = await _characteristicValueRepository.GetBySpecAsync(spec);
            characteristicValue.CharacteristicValueNullChecking();

            return _mapper.Map<CharacteristicValueResponse>(characteristicValue);
        }

        public async Task CreateAsync(CharacteristicValueRequest request, string userId)
        {
            var specExist = new CharacteristicValueGetByValueSpecification(request.Value, request.CharacteristicNameId, userId);
            var characteristicValueExist = await _characteristicValueRepository.GetBySpecAsync(specExist);
            if (characteristicValueExist != null)
                throw new AppValidationException(nameof(CharacteristicValue.Value), _errorMessagesLocalizer["CharacteristicValueExist"]);

            var characteristicName = await _characteristicNameRepository.GetByIdAsync(request.CharacteristicNameId);
            characteristicName.CharacteristicNameNullChecking();

            var user = await _userManager.FindByIdAsync(userId);
            user.UserNullChecking();

            var characteristicValue = _mapper.Map<CharacteristicValue>(request);

            characteristicValue.UserId = userId;

            await _characteristicValueRepository.AddAsync(characteristicValue);
            await _characteristicValueRepository.SaveChangesAsync();
        }

        public async Task UpdateAsync(int id, CharacteristicValueRequest request, string userId)
        {
            var specExist = new CharacteristicValueGetByValueSpecification(request.Value, request.CharacteristicNameId, userId);
            var characteristicValueExist = await _characteristicValueRepository.GetBySpecAsync(specExist);
            if (characteristicValueExist != null && id != characteristicValueExist.Id)
                throw new AppValidationException(nameof(CharacteristicValue.Value), _errorMessagesLocalizer["CharacteristicValueExist"]);

            var user = await _userManager.FindByIdAsync(userId);
            user.UserNullChecking();

            var characteristicName = await _characteristicNameRepository.GetByIdAsync(request.CharacteristicNameId);
            characteristicName.CharacteristicNameNullChecking();

            var characteristicValue = await _characteristicValueRepository.GetByIdAsync(id);
            characteristicValue.CharacteristicValueNullChecking();

            if (user.Id != characteristicValue.UserId)
                throw new AppException(_errorMessagesLocalizer["DontHavePermission"], HttpStatusCode.Forbidden);

            _mapper.Map(request, characteristicValue);

            await _characteristicValueRepository.UpdateAsync(characteristicValue);
            await _characteristicValueRepository.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var characteristicValue = await _characteristicValueRepository.GetByIdAsync(id);
            characteristicValue.CharacteristicValueNullChecking();

            await _characteristicValueRepository.DeleteAsync(characteristicValue);
            await _characteristicValueRepository.SaveChangesAsync();
        }

        public async Task<SearchResponse<CharacteristicValueResponse>> SearchAsync(SellerSearchRequest request, string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            user.UserNullChecking();

            var spec = new CharacteristicValueSearchSpecification(request.Name, request.IsAscOrder, request.OrderBy, request.IsSeller, userId);
            var count = await _characteristicValueRepository.CountAsync(spec);
            spec = new CharacteristicValueSearchSpecification(
                request.Name,
                request.IsAscOrder,
                request.OrderBy,
                 request.IsSeller,
                 userId,
                (request.Page - 1) * request.RowsPerPage,
                request.RowsPerPage);

            var characteristics = await _characteristicValueRepository.ListAsync(spec);
            var mappedCharacteristics = _mapper.Map<IEnumerable<CharacteristicValueResponse>>(characteristics);
            var response = new SearchResponse<CharacteristicValueResponse>() { Count = count, Values = mappedCharacteristics };

            return response;
        }

        public async Task DeleteAsync(IEnumerable<int> ids)
        {
            foreach (var item in ids)
            {
                var characteristicValue = await _characteristicValueRepository.GetByIdAsync(item);
                await _characteristicValueRepository.DeleteAsync(characteristicValue);
            }
            await _characteristicValueRepository.SaveChangesAsync();
        }
    }
}
