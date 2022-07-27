using AutoMapper;
using DAL;
using DAL.Entities;
using System.Drawing.Imaging;
using WebAPI.Constants;
using WebAPI.Extensions;
using WebAPI.Helpers;
using WebAPI.Interfaces.Reviews;
using WebAPI.ViewModels.Response.Reviews;

namespace WebAPI.Services.Reviews
{
    public class ReviewImageService : IReviewImageService
    {
        private readonly IRepository<ReviewImage> _reviewImageRepository;
        private readonly IMapper _mapper;

        public ReviewImageService(IRepository<ReviewImage> reviewImageRepository, IMapper mapper)
        {
            _reviewImageRepository = reviewImageRepository;
            _mapper = mapper;
        }
        public async Task<ReviewImageResponse> CreateAsync(string base64)
        {
            var img = ImageWorker.FromBase64StringToImage(base64);
            string randomFilename = Guid.NewGuid() + ".png";
            var dir = Path.Combine(Directory.GetCurrentDirectory(), ImagePath.ReviewsImagePath, randomFilename);
            img.Save(dir, ImageFormat.Png);

            var reviewImage = new ReviewImage() { Name = randomFilename };

            await _reviewImageRepository.AddAsync(reviewImage);
            await _reviewImageRepository.SaveChangesAsync();

            var response = _mapper.Map<ReviewImageResponse>(reviewImage);

            return response;
        }

        public async Task DeleteAsync(int id)
        {
            var productImage = await _reviewImageRepository.GetByIdAsync(id);
            productImage.ReviewImageNullChecking();

            var filePath = Path.Combine(Directory.GetCurrentDirectory(), ImagePath.ProductsImagePath, productImage.Name);

            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }

            await _reviewImageRepository.DeleteAsync(productImage);
            await _reviewImageRepository.SaveChangesAsync();
        }
    }
}
