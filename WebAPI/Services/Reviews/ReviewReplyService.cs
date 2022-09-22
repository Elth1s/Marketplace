using AutoMapper;
using DAL;
using DAL.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using WebAPI.Exceptions;
using WebAPI.Extensions;
using WebAPI.Interfaces.Reviews;
using WebAPI.Specifications.Reviews;
using WebAPI.ViewModels.Request.Reviews;
using WebAPI.ViewModels.Response;
using WebAPI.ViewModels.Response.Reviews;

namespace WebAPI.Services.Reviews
{
    public class ReviewReplyService : IReviewReplyService
    {
        private readonly IStringLocalizer<ErrorMessages> _errorMessagesLocalizer;

        private readonly IRepository<ReviewReply> _reviewReplyRepository;
        private readonly IRepository<Review> _reviewRepository;
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;

        public ReviewReplyService(IStringLocalizer<ErrorMessages> errorMessagesLocalizer,
        IRepository<ReviewReply> reviewReplyRepository,
            IRepository<Review> reviewRepository,
             UserManager<AppUser> userManager, IMapper mapper)
        {
            _errorMessagesLocalizer = errorMessagesLocalizer;
            _reviewReplyRepository = reviewReplyRepository;
            _reviewRepository = reviewRepository;
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task CreateAsync(ReviewReplyRequest request, string userId)
        {
            var review = await _reviewRepository.GetByIdAsync(request.ReviewId);
            review.ReviewNullChecking();

            var user = await _userManager.FindByIdAsync(userId);
            user.UserNullChecking();

            var userEmail = await _userManager.GetByEmailAsync(request.Email);
            if (userEmail != null && user.Id != userEmail.Id)
                throw new AppValidationException(
                    new ValidationError(nameof(AppUser.Email), _errorMessagesLocalizer["InvalidUserEmail"]));

            if (string.IsNullOrEmpty(user.Email))
                await _userManager.SetEmailAsync(user, request.Email);

            var reviewReply = _mapper.Map<ReviewReply>(request);
            reviewReply.UserId = userId;

            await _reviewReplyRepository.AddAsync(reviewReply);
            await _reviewReplyRepository.SaveChangesAsync();

        }

        public async Task<PaginationResponse<ReviewReplyResponse>> GetByReview(ReviewReplyForReviewRequest request)
        {
            var review = await _reviewRepository.GetByIdAsync(request.ReviewId);
            review.ReviewNullChecking();

            var spec = new ReviewReplyByReviewIdSpecification(request.ReviewId);
            var count = await _reviewReplyRepository.CountAsync(spec);

            var result = new PaginationResponse<ReviewReplyResponse>() { Count = count };

            spec = new ReviewReplyByReviewIdSpecification(request.ReviewId,
                                                       (request.Page - 1) * request.RowsPerPage,
                                                       request.RowsPerPage);

            var replies = await _reviewReplyRepository.ListAsync(spec);
            result.Values = _mapper.Map<IEnumerable<ReviewReplyResponse>>(replies);

            return result;

        }
    }
}
