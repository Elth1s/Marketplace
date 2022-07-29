using AutoMapper;
using DAL;
using DAL.Entities;
using System.Drawing.Imaging;
using WebAPI.Constants;
using WebAPI.Extensions;
using WebAPI.Helpers;
using WebAPI.Interfaces.Products;
using WebAPI.ViewModels.Response.Products;

namespace WebAPI.Services.Products
{
    public class ProductImageService : IProductImageService
    {
        private readonly IRepository<ProductImage> _productImageRepository;
        private readonly IMapper _mapper;

        public ProductImageService(IRepository<ProductImage> productImageRepository, IMapper mapper)
        {
            _productImageRepository = productImageRepository;
            _mapper = mapper;
        }

        public async Task<ProductImageResponse> CreateAsync(string base64)
        {
            var img = ImageWorker.FromBase64StringToImage(base64);
            string randomFilename = Guid.NewGuid() + ".png";
            var dir = Path.Combine(Directory.GetCurrentDirectory(), ImagePath.ProductsImagePath, randomFilename);
            img.Save(dir, ImageFormat.Png);

            var productImage = new ProductImage() { Name = randomFilename };

            await _productImageRepository.AddAsync(productImage);
            await _productImageRepository.SaveChangesAsync();

            var response = _mapper.Map<ProductImageResponse>(productImage);

            return response;
        }

        public async Task DeleteAsync(int id)
        {
            var productImage = await _productImageRepository.GetByIdAsync(id);
            productImage.ProductImageNullChecking();

            var filePath = Path.Combine(Directory.GetCurrentDirectory(), ImagePath.ProductsImagePath, productImage.Name);

            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }

            await _productImageRepository.DeleteAsync(productImage);
            await _productImageRepository.SaveChangesAsync();
        }
    }
}
