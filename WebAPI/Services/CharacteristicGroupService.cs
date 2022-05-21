using AutoMapper;
using DAL;
using DAL.Entities;
using WebAPI.Extensions;
using WebAPI.Interfaces;
using WebAPI.ViewModels.Request;
using WebAPI.ViewModels.Response;

namespace WebAPI.Services
{
    public class CharacteristicGroupService : ICharacteristicGroupService
    {
        private readonly IRepository<CharacteristicGroup> _characteristicGroupRepository;
        private readonly IMapper _mapper;

        public CharacteristicGroupService(IRepository<CharacteristicGroup> characteristicGroupRepository, IMapper mapper)
        {
            _characteristicGroupRepository = characteristicGroupRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CharacteristicGroupResponse>> GetAsync()
        {
            var characteristicGroups = await _characteristicGroupRepository.ListAsync();
            return _mapper.Map<IEnumerable<CharacteristicGroupResponse>>(characteristicGroups);
        }

        public async Task<CharacteristicGroupResponse> GetByIdAsync(int id)
        {
            var characteristicGroup = await _characteristicGroupRepository.GetByIdAsync(id);
            characteristicGroup.CharacteristicGroupNullChecking();

            return _mapper.Map<CharacteristicGroupResponse>(characteristicGroup);
        }

        public async Task CreateAsync(CharacteristicGroupRequest request)
        {
            var characteristicGroup = _mapper.Map<CharacteristicGroup>(request);

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
    }
}
