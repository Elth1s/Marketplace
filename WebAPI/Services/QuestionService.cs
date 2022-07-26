using AutoMapper;
using DAL;
using DAL.Entities;
using DAL.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using WebAPI.Extensions;
using WebAPI.Interfaces;
using WebAPI.Specifications;
using WebAPI.ViewModels.Request;
using WebAPI.ViewModels.Response;

namespace WebAPI.Services
{
    public class QuestionService : IQuestionService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IRepository<Question> _questionRepository;
        private readonly IMapper _mapper;
        public QuestionService(IRepository<Question> questionRepository, IMapper mapper,UserManager<AppUser> userManager)
        {
            _questionRepository = questionRepository;
            _mapper = mapper;
            _userManager = userManager;
        }

        public async Task<IEnumerable<QuestionResponse>> GetAllAsync()
        {
            var questions = await _questionRepository.ListAsync();

            return _mapper.Map<IEnumerable<QuestionResponse>>(questions);            
        }

        public async Task<IEnumerable<QuestionResponse>> GetAllByIdAsync(string userId)
        {
            var spec = new QuestionIncludeFullInfoSpecification(userId);
            var questions = await _questionRepository.ListAsync(spec);

            return _mapper.Map<IEnumerable<QuestionResponse>>(questions);
        }

        public async Task CreateAsync(QuestionRequest request, string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            user.UserNullChecking();

            request.UserId = userId;

            var question = _mapper.Map<Question>(request);
            await _questionRepository.AddAsync(question);
            await _questionRepository.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var question = await _questionRepository.GetByIdAsync(id);
            question.QuestionNullChecking();
            await _questionRepository.DeleteAsync(question);
            await _questionRepository.SaveChangesAsync();

        }

        
    }
}
