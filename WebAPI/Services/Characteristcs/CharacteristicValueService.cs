﻿using AutoMapper;
using DAL;
using DAL.Entities;
using WebAPI.Exceptions;
using WebAPI.Extensions;
using WebAPI.Interfaces.Characteristics;
using WebAPI.Resources;
using WebAPI.Specifications.Characteristics;
using WebAPI.ViewModels.Request.Characteristics;
using WebAPI.ViewModels.Response.Characteristics;

namespace WebAPI.Services.Characteristcs
{
    public class CharacteristicValueService : ICharacteristicValueService
    {
        private readonly IRepository<CharacteristicValue> _characteristicValueRepository;
        private readonly IRepository<CharacteristicName> _characteristicNameRepository;
        private readonly IMapper _mapper;

        public CharacteristicValueService(IRepository<CharacteristicValue> characteristicValueRepository, IRepository<CharacteristicName> characteristicNameRepository, IMapper mapper)
        {
            _characteristicValueRepository = characteristicValueRepository;
            _characteristicNameRepository = characteristicNameRepository;
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

        public async Task CreateAsync(CharacteristicValueRequest request)
        {
            var characteristicName = await _characteristicNameRepository.GetByIdAsync(request.CharacteristicNameId);
            characteristicName.CharacteristicNameNullChecking();

            var spec = new CharacteristicValueGetByValueAndCharacteristicNameIdSpecification(request.Value, request.CharacteristicNameId);
            if (await _characteristicValueRepository.GetBySpecAsync(spec) != null)
                throw new AppException(ErrorMessages.CharacteristicValueNameNotUnique);

            var characteristicValue = _mapper.Map<CharacteristicValue>(request);

            await _characteristicValueRepository.AddAsync(characteristicValue);
            await _characteristicValueRepository.SaveChangesAsync();
        }

        public async Task UpdateAsync(int id, CharacteristicValueRequest request)
        {
            var characteristicName = await _characteristicNameRepository.GetByIdAsync(request.CharacteristicNameId);
            characteristicName.CharacteristicNameNullChecking();

            var spec = new CharacteristicValueGetByValueAndCharacteristicNameIdSpecification(request.Value, request.CharacteristicNameId);
            if (await _characteristicValueRepository.GetBySpecAsync(spec) != null)
                throw new AppException(ErrorMessages.CharacteristicValueNameNotUnique);

            var characteristicValue = await _characteristicValueRepository.GetByIdAsync(id);
            characteristicValue.CharacteristicValueNullChecking();

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
    }
}
