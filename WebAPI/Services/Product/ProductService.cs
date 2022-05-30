using AutoMapper;
using DAL;
using DAL.Entities;
using System.Drawing.Imaging;
using WebAPI.Constants;
using WebAPI.Extensions;
using WebAPI.Helpers;
using WebAPI.Interfaces;
using WebAPI.Specifications;
using WebAPI.ViewModels.Request;
using WebAPI.ViewModels.Response;

namespace WebAPI.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductImageService _productImageService;
        private readonly IProductCharacteristicService _productCharacteristicService;
        private readonly IRepository<Product> _productRepository;
        private readonly IRepository<Shop> _shopRepository;
        private readonly IRepository<Category> _categoryRepository;
        private readonly IRepository<Characteristic> _characteristicRepository;
        private readonly IRepository<ProductImage> _productImageRepository;
        private readonly IRepository<ProductStatus> _productStatusRepository;
        private readonly IRepository<ProductCharacteristic> _productCharacteristicRepository;
        private readonly IMapper _mapper;

        public ProductService(
            IProductImageService productImageService,
            IProductCharacteristicService productCharacteristicService,
            IRepository<Product> productRepository,
            IRepository<Shop> shopRepository,
            IRepository<Category> categoryRepository,
            IRepository<Characteristic> characteristicRepository,
            IRepository<ProductImage> productImageRepository,
            IRepository<ProductStatus> productStatusRepository,
            IRepository<ProductCharacteristic> productCharacteristicRepository,
            IMapper mapper
            )
        {
            _productImageService = productImageService;
            _productCharacteristicService = productCharacteristicService;
            _productRepository = productRepository;
            _shopRepository = shopRepository;
            _categoryRepository = categoryRepository;
            _characteristicRepository = characteristicRepository;
            _productImageRepository = productImageRepository;
            _productStatusRepository = productStatusRepository;
            _productCharacteristicRepository = productCharacteristicRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ProductResponse>> GetAsync()
        {
            var spec = new ProductIncludeFullInfoSpecification();
            var product = await _productRepository.ListAsync(spec);

            return _mapper.Map<IEnumerable<ProductResponse>>(product);
        }

        public async Task<ProductResponse> GetByIdAsync(int id)
        {
            var spec = new ProductIncludeFullInfoSpecification(id);
            var product = await _productRepository.GetBySpecAsync(spec);
            product.ProductNullChecking();

            return _mapper.Map<ProductResponse>(product);
        }
       
        public async Task CreateAsync(ProductCreateRequest request)
        {
            var shop = await _shopRepository.GetByIdAsync(request.ShopId);
            shop.ShopNullChecking();

            var productStatus = await _productStatusRepository.GetByIdAsync(request.StatusId);
            productStatus.ProductStatusNullChecking();

            if (request.CategoryId != null)
            {
                var category = await _categoryRepository.GetByIdAsync(request.CategoryId);
                category.CategotyNullChecking();
            }

            foreach(ProductCharacteristicForProductCreateRequest productCharacteristicRequest in request.ProductCharacteristics)
            {
                var characteristic = await _characteristicRepository.GetByIdAsync(productCharacteristicRequest.CharacteristicId);
                characteristic.CharacteristicNullChecking();
            }

            var product = _mapper.Map<Product>(request);

            product = await _productRepository.AddAsync(product);
            await _productRepository.SaveChangesAsync();

            foreach(ProductImageForProductCreateRequest imageRequest in request.ProductImages)
            {
                var productImage = _mapper.Map<ProductImageRequest>(imageRequest);
                productImage.ProductId = product.Id;

               await _productImageService.CreateAsync(productImage);
            }

            foreach (ProductCharacteristicForProductCreateRequest characteristicRequest in request.ProductCharacteristics)
            {
                var productCharacteristic = _mapper.Map<ProductCharacteristicRequest>(characteristicRequest);
                productCharacteristic.ProductId = product.Id;

                await _productCharacteristicService.CreateAsync(productCharacteristic);
            }
        }

        public async Task UpdateAsync(int id, ProductUpdateRequest request)
        {
            var product = await _productRepository.GetByIdAsync(id);
            product.ProductNullChecking();

            var shop = await _shopRepository.GetByIdAsync(request.ShopId);
            shop.ShopNullChecking();

            var productStatus = await _productStatusRepository.GetByIdAsync(request.StatusId);
            productStatus.ProductStatusNullChecking();

            if (request.CategoryId != null)
            {
                var category = await _categoryRepository.GetByIdAsync(request.CategoryId);
                category.CategotyNullChecking();
            }

            _mapper.Map(request, product);

            await _productRepository.UpdateAsync(product);
            await _productRepository.SaveChangesAsync();

            foreach (ProductImageForProductUpdateRequest imageRequest in request.ProductImages)
            {
                var productImage = _mapper.Map<ProductImageRequest>(imageRequest);
                await _productImageService.UpdateAsync(imageRequest.Id, productImage);
            }

            foreach(ProductCharacteristicForProductUpdateRequest characteristicRequest in request.ProductCharacteristics)
            {
                var productCharacteristic = _mapper.Map<ProductCharacteristicRequest>(characteristicRequest);
                await _productCharacteristicService.UpdateAsync(characteristicRequest.Id, productCharacteristic);
            }
        }

        public async Task DeleteAsync(int id)
        {
            var product = await _productRepository.GetByIdAsync(id);
            product.ProductNullChecking();

            var specProductImage = new ProductImageGetByProductSpecification(product.Id);
            var productImages = await _productImageRepository.ListAsync(specProductImage);

            if(productImages != null)
            {
                foreach (ProductImage productImage in productImages)
                {
                    await _productImageService.DeleteAsync(productImage.Id);
                }
            }

            var specProductCharacteristic = new ProductCharacteristicGetByProductSpecification(product.Id);
            var productCharacteristics = await _productCharacteristicRepository.ListAsync(specProductCharacteristic);

            if (productCharacteristics != null)
            {
                foreach (ProductCharacteristic productCharacteristic in productCharacteristics)
                {
                    await _productCharacteristicService.DeleteAsync(productCharacteristic.Id);
                }
            }

            await _productRepository.DeleteAsync(product);
            await _productRepository.SaveChangesAsync();
        }
    }
}
