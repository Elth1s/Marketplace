using AutoMapper;
using DAL;
using DAL.Entities;
using DAL.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using WebAPI.Exceptions;
using WebAPI.Extensions;
using WebAPI.Interfaces.Questions;
using WebAPI.Resources;
using WebAPI.Specifications.Questions;
using WebAPI.ViewModels.Request.Questions;
using WebAPI.ViewModels.Response;
using WebAPI.ViewModels.Response.Questions;

namespace WebAPI.Services.Questions
{
    public class QuestionReplyService : IQuestionReplyService
    {
        private readonly IRepository<QuestionReply> _questionReplyRepository;
        private readonly IRepository<Question> _questionRepository;
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;
        public QuestionReplyService(
            IRepository<QuestionReply> questionReplyRepository,
            IRepository<Question> questionRepository,
            UserManager<AppUser> userManager,
            IMapper mapper)
        {
            _questionReplyRepository = questionReplyRepository;
            _questionRepository = questionRepository;
            _userManager = userManager;
            _mapper = mapper;
        }
        public async Task CreateAsync(QuestionReplyRequest request, string userId)
        {
            var question = await _questionRepository.GetByIdAsync(request.QuestionId);
            question.QuestionNullChecking();

            var user = await _userManager.FindByIdAsync(userId);
            user.UserNullChecking();

            var userEmail = await _userManager.FindByEmailAsync(request.Email);
            if (userEmail != null && user.Id != userEmail.Id)
                throw new AppValidationException(new ValidationError(nameof(AppUser.Email), ErrorMessages.InvalidUserEmail));

            if (string.IsNullOrEmpty(user.Email))
                await _userManager.SetEmailAsync(user, request.Email);

            var questionReply = _mapper.Map<QuestionReply>(request);
            questionReply.UserId = userId;

            await _questionReplyRepository.AddAsync(questionReply);
            await _questionReplyRepository.SaveChangesAsync();
        }

        public async Task<PaginationResponse<QuestionReplyResponse>> GetByQuestion(QuestionReplyForQuestionRequest request)
        {
            var question = await _questionRepository.GetByIdAsync(request.QuestionId);
            question.QuestionNullChecking();

            var spec = new QuestionReplyByQuestionIdSpecification(request.QuestionId);
            var count = await _questionReplyRepository.CountAsync(spec);

            var result = new PaginationResponse<QuestionReplyResponse>() { Count = count };

            spec = new QuestionReplyByQuestionIdSpecification(request.QuestionId,
                                                       (request.Page - 1) * request.RowsPerPage,
                                                       request.RowsPerPage);

            var replies = await _questionReplyRepository.ListAsync(spec);
            result.Values = _mapper.Map<IEnumerable<QuestionReplyResponse>>(replies);

            return result;
        }
    }
}
