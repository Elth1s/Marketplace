using AutoMapper;
using DAL;
using DAL.Entities;
using System.Drawing.Imaging;
using WebAPI.Constants;
using WebAPI.Extensions;
using WebAPI.Helpers;
using WebAPI.Interfaces.Questions;
using WebAPI.ViewModels.Response.Questions;

namespace WebAPI.Services.Questions
{
    public class QuestionImageService : IQuestionImageService
    {
        private readonly IRepository<QuestionImage> _questionImageRepository;
        private readonly IMapper _mapper;

        public QuestionImageService(IRepository<QuestionImage> questionImageRepository, IMapper mapper)
        {
            _questionImageRepository = questionImageRepository;
            _mapper = mapper;
        }
        public async Task<QuestionImageResponse> CreateAsync(string base64)
        {
            var img = ImageWorker.FromBase64StringToImage(base64);
            string randomFilename = Guid.NewGuid() + ".png";
            var dir = Path.Combine(Directory.GetCurrentDirectory(), ImagePath.QuestionsImagePath, randomFilename);
            img.Save(dir, ImageFormat.Png);

            var questionImage = new QuestionImage() { Name = randomFilename };

            await _questionImageRepository.AddAsync(questionImage);
            await _questionImageRepository.SaveChangesAsync();

            var response = _mapper.Map<QuestionImageResponse>(questionImage);

            return response;
        }

        public async Task DeleteAsync(int id)
        {
            var qusetionImage = await _questionImageRepository.GetByIdAsync(id);
            qusetionImage.QuestionImageNullChecking();

            var filePath = Path.Combine(Directory.GetCurrentDirectory(), ImagePath.QuestionsImagePath, qusetionImage.Name);

            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }

            await _questionImageRepository.DeleteAsync(qusetionImage);
            await _questionImageRepository.SaveChangesAsync();
        }
    }
}
