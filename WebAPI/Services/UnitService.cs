using AutoMapper;
using DAL;
using DAL.Entities;
using WebAPI.Extensions;
using WebAPI.Interfaces;
using WebAPI.ViewModels.Request;
using WebAPI.ViewModels.Response;

namespace WebAPI.Services
{
    public class UnitService : IUnitService
    {
        private readonly IRepository<Unit> _unitRepository;
        private readonly IMapper _mapper;

        public UnitService(
            IRepository<Unit> unitRepository,
            IMapper mapper
            )
        {
            _unitRepository = unitRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<UnitResponse>> GetAllAsync()
        {
            var units = await _unitRepository.ListAsync();

            var response = units.Select(c => _mapper.Map<UnitResponse>(c));
            return response;
        }
        public async Task<UnitResponse> GetByIdAsync(int id)
        {
            var unit = await _unitRepository.GetByIdAsync(id);
            unit.UnitNullChecking();

            var response = _mapper.Map<UnitResponse>(unit);
            return response;
        }

        public async Task CreateAsync(UnitRequest request)
        {
            var unit = _mapper.Map<Unit>(request);

            await _unitRepository.AddAsync(unit);
            await _unitRepository.SaveChangesAsync();
        }

        public async Task UpdateAsync(int id, UnitRequest request)
        {
            var unit = await _unitRepository.GetByIdAsync(id);
            unit.UnitNullChecking();

            _mapper.Map(request, unit);

            await _unitRepository.UpdateAsync(unit);
            await _unitRepository.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var unit = await _unitRepository.GetByIdAsync(id);
            unit.UnitNullChecking();

            await _unitRepository.DeleteAsync(unit);
            await _unitRepository.SaveChangesAsync();
        }
    }
}
