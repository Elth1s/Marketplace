using AutoMapper;
using DAL;
using DAL.Entities;
using Microsoft.AspNetCore.Identity;
using WebAPI.Extensions;
using WebAPI.Interfaces.Characteristics;
using WebAPI.Specifications.Characteristics;
using WebAPI.ViewModels.Request;
using WebAPI.ViewModels.Request.Characteristics;
using WebAPI.ViewModels.Response;
using WebAPI.ViewModels.Response.Characteristics;

namespace WebAPI.Services.Characteristcs
{
    public class CharacteristicGroupService : ICharacteristicGroupService
    {
        private readonly IRepository<CharacteristicGroup> _characteristicGroupRepository;
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;

        public CharacteristicGroupService(IRepository<CharacteristicGroup> characteristicGroupRepository, UserManager<AppUser> userManager, IMapper mapper)
        {
            _characteristicGroupRepository = characteristicGroupRepository;
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CharacteristicGroupResponse>> GetAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            user.UserNullChecking();

            var spec = new CharacteristicGroupGetByUserIdSpecification(userId);
            var characteristicGroups = await _characteristicGroupRepository.ListAsync(spec);
            return _mapper.Map<IEnumerable<CharacteristicGroupResponse>>(characteristicGroups);
        }

        public async Task<AdminSearchResponse<CharacteristicGroupResponse>> SearchCharacteristicGroupsAsync(AdminSearchRequest request)
        {
            var spec = new CharacteristicGroupSearchSpecification(request.Name, request.IsAscOrder, request.OrderBy);
            var count = await _characteristicGroupRepository.CountAsync(spec);

            spec = new CharacteristicGroupSearchSpecification(request.Name,
                request.IsAscOrder,
                request.OrderBy,
                (request.Page - 1) * request.RowsPerPage,
                request.RowsPerPage);

            var characteristicGroups = await _characteristicGroupRepository.ListAsync(spec);
            var mappedCountries = _mapper.Map<IEnumerable<CharacteristicGroupResponse>>(characteristicGroups);
            var response = new AdminSearchResponse<CharacteristicGroupResponse>() { Count = count, Values = mappedCountries };

            return response;
        }

        public async Task<CharacteristicGroupResponse> GetByIdAsync(int id)
        {
            var characteristicGroup = await _characteristicGroupRepository.GetByIdAsync(id);
            characteristicGroup.CharacteristicGroupNullChecking();

            return _mapper.Map<CharacteristicGroupResponse>(characteristicGroup);
        }

        public async Task CreateAsync(CharacteristicGroupRequest request, string userId)
        {
            var characteristicGroup = _mapper.Map<CharacteristicGroup>(request);

            var user = await _userManager.FindByIdAsync(userId);
            user.UserNullChecking();
            characteristicGroup.UserId = userId;

            await _characteristicGroupRepository.AddAsync(characteristicGroup);
            await _characteristicGroupRepository.SaveChangesAsync();
        }

        public async Task UpdateAsync(int id, CharacteristicGroupRequest request)
        {
            var characteristicGroup = await _characteristicGroupRepository.GetByIdAsync(id);
            characteristicGroup.CharacteristicGroupNullChecking();

            _mapper.Map(request, characteristicGroup);

            await _characteristicGroupRepository.UpdateAsync(characteristicGroup);
            await _characteristicGroupRepository.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var characteristicGroup = await _characteristicGroupRepository.GetByIdAsync(id);
            characteristicGroup.CharacteristicGroupNullChecking();

            await _characteristicGroupRepository.DeleteAsync(characteristicGroup);
            await _characteristicGroupRepository.SaveChangesAsync();
        }

        public async Task DeleteAsync(IEnumerable<int> ids)
        {
            foreach (var item in ids)
            {
                var country = await _characteristicGroupRepository.GetByIdAsync(item);
                await _characteristicGroupRepository.DeleteAsync(country);
            }
            await _characteristicGroupRepository.SaveChangesAsync();
        }
    }
}
