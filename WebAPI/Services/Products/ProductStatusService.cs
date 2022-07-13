using AutoMapper;
using DAL;
using DAL.Entities;
using WebAPI.Extensions;
using WebAPI.Interfaces.Products;
using WebAPI.Specifications.Products;
using WebAPI.ViewModels.Request;
using WebAPI.ViewModels.Request.Products;
using WebAPI.ViewModels.Response;
using WebAPI.ViewModels.Response.Products;

namespace WebAPI.Services.Products
{
    public class ProductStatusService : IProductStatusService
    {
        private readonly IRepository<ProductStatus> _productStatusRepository;
        private readonly IMapper _mapper;

        public ProductStatusService(IRepository<ProductStatus> countryRepository, IMapper mapper)
        {
            _productStatusRepository = countryRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ProductStatusResponse>> GetAsync()
        {
            var productStatus = await _productStatusRepository.ListAsync();

            return _mapper.Map<IEnumerable<ProductStatusResponse>>(productStatus);
        }

        public async Task<ProductStatusResponse> GetByIdAsync(int id)
        {
            var productStatus = await _productStatusRepository.GetByIdAsync(id);
            productStatus.ProductStatusNullChecking();

            return _mapper.Map<ProductStatusResponse>(productStatus);
        }

        public async Task CreateAsync(ProductStatusRequest request)
        {
            var productStatus = _mapper.Map<ProductStatus>(request);

            await _productStatusRepository.AddAsync(productStatus);
            await _productStatusRepository.SaveChangesAsync();
        }

        public async Task UpdateAsync(int id, ProductStatusRequest request)
        {
            var productStatus = await _productStatusRepository.GetByIdAsync(id);
            productStatus.ProductStatusNullChecking();

            _mapper.Map(request, productStatus);

            await _productStatusRepository.UpdateAsync(productStatus);
            await _productStatusRepository.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var productStatus = await _productStatusRepository.GetByIdAsync(id);
            productStatus.ProductStatusNullChecking();

            await _productStatusRepository.DeleteAsync(productStatus);
            await _productStatusRepository.SaveChangesAsync();
        }

        public async Task<AdminSearchResponse<ProductStatusResponse>> SearchProductStatusesAsync(AdminSearchRequest request)
        {
            var spec = new ProductStatusSearchSpecification(request.Name, request.IsAscOrder, request.OrderBy);
            var statuses = await _productStatusRepository.ListAsync(spec);
            var mappedStatuses = _mapper.Map<IEnumerable<ProductStatusResponse>>(statuses);
            var response = new AdminSearchResponse<ProductStatusResponse>() { Count = statuses.Count };

            response.Values = mappedStatuses.Skip((request.Page - 1) * request.RowsPerPage).Take(request.RowsPerPage);

            return response;
        }

        public async Task DeleteProductStatusesAsync(IEnumerable<int> ids)
        {
            foreach (var item in ids)
            {
                var status = await _productStatusRepository.GetByIdAsync(item);
                await _productStatusRepository.DeleteAsync(status);
            }
            await _productStatusRepository.SaveChangesAsync();
        }
    }
}
