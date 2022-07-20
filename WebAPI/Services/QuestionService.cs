using AutoMapper;
using DAL;
using DAL.Entities;
using WebAPI.Interfaces;
using WebAPI.ViewModels.Request;

namespace WebAPI.Services
{
    public class QuestionService : IQuestionService
    {
        private readonly IRepository<Question> _questionRepository;
        private readonly IMapper _mapper;
        public QuestionService(IRepository<Question> questionRepository, IMapper mapper)
        {
            _questionRepository = questionRepository;
            _mapper = mapper;
        }
        public Task CreateAsync(QuestionRequest request, string userId)
        {


            throw new NotImplementedException();
        }

        public Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
