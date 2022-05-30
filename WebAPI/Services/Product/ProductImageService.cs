using AutoMapper;
using DAL;
using DAL.Entities;
using System.Drawing.Imaging;
using WebAPI.Constants;
using WebAPI.Extensions;
using WebAPI.Helpers;
using WebAPI.Interfaces;
using WebAPI.ViewModels.Request;
using WebAPI.ViewModels.Response;

namespace WebAPI.Services
{
    public class ProductImageService : IProductImageService
    {
        private readonly IRepository<ProductImage> _productImageRepository;
        private readonly IMapper _mapper;

        public ProductImageService(
            IRepository<ProductImage> productImageRepository,
            IMapper mapper)
        {
            _productImageRepository = productImageRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ProductImageResponse>> GetAsync()
        {
            var productImage = await _productImageRepository.ListAsync();

            return _mapper.Map<IEnumerable<ProductImageResponse>>(productImage);
        }

        public async Task<ProductImageResponse> GetByIdAsync(int id)
        {
            var productImage = await _productImageRepository.GetByIdAsync(id);
            productImage.ProductImageNullChecking();

            return _mapper.Map<ProductImageResponse>(productImage);
        }

        public async Task CreateAsync(ProductImageRequest request)
        {
            var productImage = _mapper.Map<ProductImage>(request);

            if (!string.IsNullOrEmpty(request.Name))
            {
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), request.Name.Replace(ImagePath.RequestProductsImagePath, ImagePath.ProductsImagePath));

                if (!File.Exists(filePath))
                {
                    var img = ImageWorker.FromBase64StringToImage(request.Name);
                    string randomFilename = Path.GetRandomFileName() + ".jpg";
                    var dir = Path.Combine(Directory.GetCurrentDirectory(), ImagePath.ProductsImagePath, randomFilename);
                    img.Save(dir, ImageFormat.Jpeg);

                    productImage.Name = randomFilename;
                }
            }

            await _productImageRepository.AddAsync(productImage);
            await _productImageRepository.SaveChangesAsync();
        }

        public async Task UpdateAsync(int id, ProductImageRequest request)
        {
            var productImage = await _productImageRepository.GetByIdAsync(id);
            productImage.ProductImageNullChecking();

            if (!string.IsNullOrEmpty(request.Name))
            {
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), request.Name.Replace(ImagePath.RequestProductsImagePath, ImagePath.ProductsImagePath));

                if (!File.Exists(filePath))
                {
                    if (request != null)
                    {
                        filePath = Path.Combine(Directory.GetCurrentDirectory(), ImagePath.CategoriesImagePath, request.Name);

                        if (File.Exists(filePath))
                        {
                            File.Delete(filePath);
                        }
                    }

                    var img = ImageWorker.FromBase64StringToImage(request.Name);
                    string randomFilename = Path.GetRandomFileName() + ".jpg";
                    var dir = Path.Combine(Directory.GetCurrentDirectory(), ImagePath.ProductsImagePath, randomFilename);
                    img.Save(dir, ImageFormat.Jpeg);

                    productImage.Name = randomFilename;
                }
            }

            await _productImageRepository.UpdateAsync(productImage);
            await _productImageRepository.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var productImage = await _productImageRepository.GetByIdAsync(id);

            if (!string.IsNullOrEmpty(productImage.Name))
            {
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), ImagePath.ProductsImagePath, productImage.Name);

                if (File.Exists(filePath))
                {
                    if (File.Exists(filePath))
                    {
                        File.Delete(filePath);
                    }
                }
            }

            await _productImageRepository.DeleteAsync(productImage);
            await _productImageRepository.SaveChangesAsync();
        }
    }
}
