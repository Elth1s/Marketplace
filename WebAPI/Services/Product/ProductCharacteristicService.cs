using AutoMapper;
using DAL;
using DAL.Entities;
using WebAPI.Extensions;
using WebAPI.Interfaces;
using WebAPI.ViewModels.Request;
using WebAPI.ViewModels.Response;

namespace WebAPI.Services
{
    public class ProductCharacteristicService : IProductCharacteristicService
    {
        private readonly IRepository<ProductCharacteristic> _productCharacteristicRepository;
        private readonly IMapper _mapper;

        public ProductCharacteristicService(
            IRepository<ProductCharacteristic> productCharacteristicRepository, 
            IMapper mapper)
        {
            _productCharacteristicRepository = productCharacteristicRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ProductCharacteristicResponse>> GetAsync()
        {
            var productCharacteristic = await _productCharacteristicRepository.ListAsync();
            return _mapper.Map<IEnumerable<ProductCharacteristicResponse>>(productCharacteristic);
        }

        public async Task<ProductCharacteristicResponse> GetByIdAsync(int id)
        {
            var productCharacteristic = await _productCharacteristicRepository.ListAsync();
            return _mapper.Map<ProductCharacteristicResponse>(productCharacteristic);
        }

        public async Task CreateAsync(ProductCharacteristicRequest request)
        {
            var productCharacteristic = _mapper.Map<ProductCharacteristic>(request);

            await _productCharacteristicRepository.AddAsync(productCharacteristic);
            await _productCharacteristicRepository.SaveChangesAsync();
        }

        public async Task UpdateAsync(int id, ProductCharacteristicRequest request)
        {
            var productCharacteristic = _mapper.Map<ProductCharacteristic>(request);

            await _productCharacteristicRepository.UpdateAsync(productCharacteristic);
            await _productCharacteristicRepository.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var productCharacteristic = await _productCharacteristicRepository.GetByIdAsync(id);
            productCharacteristic.ProductCharacteristicNullChecking();

            await _productCharacteristicRepository.DeleteAsync(productCharacteristic);
            await _productCharacteristicRepository.SaveChangesAsync();
        }
    }
}
