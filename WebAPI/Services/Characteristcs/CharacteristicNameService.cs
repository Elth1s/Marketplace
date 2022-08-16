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
    public class CharacteristicNameService : ICharacteristicNameService
    {
        private readonly IStringLocalizer<ErrorMessages> _errorMessagesLocalizer;
        private readonly IRepository<CharacteristicName> _characteristicNameRepository;
        private readonly IRepository<CharacteristicGroup> _characteristicGroupRepository;
        private readonly IRepository<Unit> _unitRepository;
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;

        public CharacteristicNameService(IStringLocalizer<ErrorMessages> errorMessagesLocalizer,
            IRepository<CharacteristicName> characteristicNameRepository,
            IRepository<CharacteristicGroup> characteristicGroupRepository,
            IRepository<Unit> unitRepository,
            UserManager<AppUser> userManager,
            IMapper mapper)
        {
            _errorMessagesLocalizer = errorMessagesLocalizer;
            _characteristicNameRepository = characteristicNameRepository;
            _characteristicGroupRepository = characteristicGroupRepository;
            _unitRepository = unitRepository;
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CharacteristicNameResponse>> GetAsync(string userId)
        {
            var spec = new CharacteristicNameGetByUserIdSpecification(userId);
            var characteristicNames = await _characteristicNameRepository.ListAsync(spec);

            return _mapper.Map<IEnumerable<CharacteristicNameResponse>>(characteristicNames);
        }

        public async Task<AdminSearchResponse<CharacteristicNameResponse>> SearchAsync(SellerSearchRequest request, string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            user.UserNullChecking();

            var spec = new CharacteristicNameSearchSpecification(request.Name, request.IsAscOrder, request.OrderBy, request.IsSeller, userId);
            var count = await _characteristicNameRepository.CountAsync(spec);

            spec = new CharacteristicNameSearchSpecification(
                request.Name,
                request.IsAscOrder,
                request.OrderBy,
                request.IsSeller,
                userId,
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

        public async Task CreateAsync(CharacteristicNameRequest request, string userId)
        {
            var specExist = new CharacteristicNameGetByNameSpecification(request.CharacteristicGroupId, request.Name, request.UnitId, userId);
            var characteristicNameExist = await _characteristicNameRepository.GetBySpecAsync(specExist);
            if (characteristicNameExist != null)
                throw new AppValidationException(nameof(CharacteristicName.Name), _errorMessagesLocalizer["CharacteristicNameExist"]);

            var characteristicGroup = await _characteristicGroupRepository.GetByIdAsync(request.CharacteristicGroupId);
            characteristicGroup.CharacteristicGroupNullChecking();

            var user = await _userManager.FindByIdAsync(userId);
            user.UserNullChecking();

            if (request.UnitId != null)
            {
                var unit = await _unitRepository.GetByIdAsync(request.UnitId);
                unit.UnitNullChecking();
            }

            var characteristicName = _mapper.Map<CharacteristicName>(request);

            characteristicName.UserId = userId;

            await _characteristicNameRepository.AddAsync(characteristicName);
            await _characteristicNameRepository.SaveChangesAsync();
        }

        public async Task UpdateAsync(int id, CharacteristicNameRequest request, string userId)
        {
            var specExist = new CharacteristicNameGetByNameSpecification(request.CharacteristicGroupId, request.Name, request.UnitId, userId);
            var characteristicNameExist = await _characteristicNameRepository.GetBySpecAsync(specExist);
            if (characteristicNameExist != null && id != characteristicNameExist.Id)
                throw new AppValidationException(nameof(CharacteristicName.Name), _errorMessagesLocalizer["CharacteristicNameExist"]);

            var user = await _userManager.FindByIdAsync(userId);
            user.UserNullChecking();

            var characteristicGroup = await _characteristicGroupRepository.GetByIdAsync(request.CharacteristicGroupId);
            characteristicGroup.CharacteristicGroupNullChecking();

            if (request.UnitId != null)
            {
                var unit = await _unitRepository.GetByIdAsync(request.UnitId);
                unit.UnitNullChecking();
            }



            var characteristicName = await _characteristicNameRepository.GetByIdAsync(id);
            characteristicName.CharacteristicNameNullChecking();

            if (user.Id != characteristicName.UserId)
                throw new AppException(_errorMessagesLocalizer["DontHavePermission"], HttpStatusCode.Forbidden);

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
