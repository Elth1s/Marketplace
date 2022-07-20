using AutoMapper;
using DAL;
using DAL.Entities;
using WebAPI.Extensions;
using WebAPI.Interfaces;
using WebAPI.Specifications.Units;
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

            var response = _mapper.Map<IEnumerable<UnitResponse>>(units);
            return response;
        }

        public async Task<AdminSearchResponse<UnitResponse>> SearchUnitsAsync(AdminSearchRequest request)
        {
            var spec = new UnitSearchSpecification(request.Name, request.IsAscOrder, request.OrderBy);
            var count = await _unitRepository.CountAsync(spec);
            spec = new UnitSearchSpecification(
                request.Name,
                request.IsAscOrder,
                request.OrderBy,
                (request.Page - 1) * request.RowsPerPage,
                request.RowsPerPage);
            var units = await _unitRepository.ListAsync(spec);
            var mappedUnits = _mapper.Map<IEnumerable<UnitResponse>>(units);
            var response = new AdminSearchResponse<UnitResponse>() { Count = count, Values = mappedUnits };

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
        public async Task DeleteUnitsAsync(IEnumerable<int> ids)
        {
            foreach (var item in ids)
            {
                var unit = await _unitRepository.GetByIdAsync(item);
                //unit.UnitNullChecking();
                await _unitRepository.DeleteAsync(unit);
            }
            await _unitRepository.SaveChangesAsync();
        }
    }
}
