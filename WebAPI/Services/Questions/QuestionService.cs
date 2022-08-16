using AutoMapper;
using DAL;
using DAL.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using WebAPI.Exceptions;
using WebAPI.Extensions;
using WebAPI.Interfaces.Questions;
using WebAPI.Specifications.Products;
using WebAPI.Specifications.Questions;
using WebAPI.ViewModels.Request.Questions;
using WebAPI.ViewModels.Response;
using WebAPI.ViewModels.Response.Questions;

namespace WebAPI.Services.Questions
{
    public class QuestionService : IQuestionService
    {
        private readonly IStringLocalizer<ErrorMessages> _errorMessagesLocalizer;
        private readonly UserManager<AppUser> _userManager;
        private readonly IRepository<Question> _questionRepository;
        private readonly IRepository<QuestionImage> _questionImageRepository;
        private readonly IRepository<QuestionVotes> _questionVotesRepository;
        private readonly IRepository<Product> _productRepository;
        private readonly IMapper _mapper;
        public QuestionService(IStringLocalizer<ErrorMessages> errorMessagesLocalizer,
            IRepository<Question> questionRepository,
            IRepository<QuestionImage> questionImageRepository,
            IRepository<Product> productRepository,
            IRepository<QuestionVotes> questionVotesRepository,
            UserManager<AppUser> userManager,
             IMapper mapper)
        {
            _errorMessagesLocalizer = errorMessagesLocalizer;
            _questionRepository = questionRepository;
            _questionImageRepository = questionImageRepository;
            _productRepository = productRepository;
            _questionVotesRepository = questionVotesRepository;
            _userManager = userManager;
            _mapper = mapper;
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
            var spec = new ProductGetByUrlSlugSpecification(request.ProductSlug);
            var product = await _productRepository.GetBySpecAsync(spec);
            product.ProductNullChecking();

            var user = await _userManager.FindByIdAsync(userId);
            user.UserNullChecking();

            var userEmail = await _userManager.FindByEmailAsync(request.Email);
            if (userEmail != null && user.Id != userEmail.Id)
                throw new AppValidationException(
                    new ValidationError(nameof(AppUser.Email), _errorMessagesLocalizer["InvalidUserEmail"]));
            if (string.IsNullOrEmpty(user.Email))
                await _userManager.SetEmailAsync(user, request.Email);

            var question = _mapper.Map<Question>(request);
            question.User = user;
            question.Product = product;

            await _questionRepository.AddAsync(question);
            await _questionRepository.SaveChangesAsync();

            if (request.Images != null)
            {
                foreach (var item in request.Images)
                {
                    var image = await _questionImageRepository.GetByIdAsync(item);
                    if (image == null)
                        continue;

                    image.QuestionId = question.Id;
                    await _questionImageRepository.UpdateAsync(image);
                    await _questionImageRepository.SaveChangesAsync();
                }
            }
        }

        public async Task<QuestionResponse> GetByIdAsync(int id, string userId)
        {
            var questionSpec = new QuestionGetByIdSpecification(id);
            var question = await _questionRepository.GetBySpecAsync(questionSpec);
            question.QuestionNullChecking();

            var result = _mapper.Map<QuestionResponse>(question);
            var spec = new QuestionVotesGetUserVoteSpecification(id, userId);
            var vote = await _questionVotesRepository.GetBySpecAsync(spec);
            result.IsLiked = vote != null && vote.IsLike;
            result.IsDisliked = vote != null && !vote.IsLike;

            return result;
        }

        public async Task<PaginationResponse<QuestionResponse>> GetByProductSlugAsync(QuestionForProductRequest request, string userId)
        {
            var productSpec = new ProductGetByUrlSlugSpecification(request.ProductSlug);
            var product = await _productRepository.GetBySpecAsync(productSpec);
            product.ProductNullChecking();

            var questionSpec = new QuestionsByProductIdSpecification(product.Id);
            var count = await _questionRepository.CountAsync(questionSpec);

            var result = new PaginationResponse<QuestionResponse>() { Count = count };

            questionSpec = new QuestionsByProductIdSpecification(
                product.Id,
                (request.Page - 1) * request.RowsPerPage,
                    request.RowsPerPage);
            var questions = await _questionRepository.ListAsync(questionSpec);

            result.Values = _mapper.Map<IEnumerable<QuestionResponse>>(questions);
            if (!string.IsNullOrEmpty(userId))
            {
                foreach (var item in result.Values)
                {
                    var spec = new QuestionVotesGetUserVoteSpecification(item.Id, userId);
                    var vote = await _questionVotesRepository.GetBySpecAsync(spec);
                    item.IsLiked = vote != null && vote.IsLike;
                    item.IsDisliked = vote != null && !vote.IsLike;
                }
            }

            return result;
        }

        public async Task Like(int id, string userId)
        {
            var questionSpec = new QuestionGetByIdSpecification(id);
            var question = await _questionRepository.GetBySpecAsync(questionSpec);
            question.QuestionNullChecking();

            var user = await _userManager.FindByIdAsync(userId);
            user.UserNullChecking();

            var spec = new QuestionVotesGetUserVoteSpecification(id, userId);
            var vote = await _questionVotesRepository.GetBySpecAsync(spec);
            if (vote == null)
            {
                vote = new QuestionVotes() { QuestionId = id, UserId = userId, IsLike = true };
                await _questionVotesRepository.AddAsync(vote);
            }
            else if (!vote.IsLike)
            {
                vote.IsLike = true;
                await _questionVotesRepository.UpdateAsync(vote);
            }
            else
                await _questionVotesRepository.DeleteAsync(vote);

            await _questionVotesRepository.SaveChangesAsync();
        }

        public async Task Dislike(int id, string userId)
        {
            var questionSpec = new QuestionGetByIdSpecification(id);
            var question = await _questionRepository.GetBySpecAsync(questionSpec);
            question.QuestionNullChecking();

            var user = await _userManager.FindByIdAsync(userId);
            user.UserNullChecking();

            var spec = new QuestionVotesGetUserVoteSpecification(id, userId);
            var vote = await _questionVotesRepository.GetBySpecAsync(spec);
            if (vote == null)
            {
                vote = new QuestionVotes() { QuestionId = id, UserId = userId };
                await _questionVotesRepository.AddAsync(vote);
            }
            else if (vote.IsLike)
            {
                vote.IsLike = false;
                await _questionVotesRepository.UpdateAsync(vote);
            }
            else
                await _questionVotesRepository.DeleteAsync(vote);

            await _questionVotesRepository.SaveChangesAsync();
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
