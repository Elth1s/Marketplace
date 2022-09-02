using AutoMapper;
using DAL;
using DAL.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using WebAPI.Exceptions;
using WebAPI.Extensions;
using WebAPI.Interfaces.Reviews;
using WebAPI.Specifications.Products;
using WebAPI.Specifications.Reviews;
using WebAPI.ViewModels.Request.Reviews;
using WebAPI.ViewModels.Response;
using WebAPI.ViewModels.Response.Reviews;

namespace WebAPI.Services.Reviews
{
    public class ReviewService : IReviewService
    {
        private readonly IStringLocalizer<ErrorMessages> _errorMessagesLocalizer;
        private readonly IRepository<Review> _reviewRepository;
        private readonly IRepository<ReviewImage> _reviewImageRepository;
        private readonly IRepository<ReviewVotes> _reviewVotesRepository;
        private readonly IRepository<Product> _productRepository;
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;

        public ReviewService(IStringLocalizer<ErrorMessages> errorMessagesLocalizer,
            IRepository<Review> reviewRepository,
            IRepository<ReviewImage> reviewImageRepository,
            IRepository<ReviewVotes> reviewVotesRepository,
            IRepository<Product> productRepository,
            UserManager<AppUser> userManager,
            IMapper mapper)
        {
            _errorMessagesLocalizer = errorMessagesLocalizer;
            _reviewRepository = reviewRepository;
            _reviewImageRepository = reviewImageRepository;
            _reviewVotesRepository = reviewVotesRepository;
            _productRepository = productRepository;
            _userManager = userManager;
            _mapper = mapper;
        }
        public async Task CreateAsync(ReviewRequest request, string userId)
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

            //var isOrdered = await _userManager.IsOrderedByUserAsync(userId, product.Id);
            //if (!isOrdered)
            //    throw new AppException(_errorMessagesLocalizer["ReviewOrder"]);

            var review = _mapper.Map<Review>(request);
            review.User = user;
            review.Product = product;

            await _reviewRepository.AddAsync(review);
            await _reviewRepository.SaveChangesAsync();


            if (request.Images != null)
            {
                foreach (var item in request.Images)
                {
                    var image = await _reviewImageRepository.GetByIdAsync(item);
                    if (image == null)
                        continue;

                    image.ReviewId = review.Id;
                    await _reviewImageRepository.UpdateAsync(image);
                    await _reviewImageRepository.SaveChangesAsync();
                }
            }
        }

        public async Task<IEnumerable<ReviewResponse>> GetAsync()
        {
            var spec = new ReviewIncludeInfoSpecification();
            var reviews = await _reviewRepository.ListAsync(spec);
            var result = _mapper.Map<IEnumerable<ReviewResponse>>(reviews);
            return result;
        }

        public async Task<ReviewResponse> GetByIdAsync(int id, string userId)
        {
            var reviewSpec = new ReviewGetByIdSpecification(id);
            var review = await _reviewRepository.GetBySpecAsync(reviewSpec);
            review.ReviewNullChecking();

            var result = _mapper.Map<ReviewResponse>(review);
            var spec = new ReviewVotesGetUserVoteSpecification(id, userId);
            var vote = await _reviewVotesRepository.GetBySpecAsync(spec);
            result.IsLiked = vote != null && vote.IsLike;
            result.IsDisliked = vote != null && !vote.IsLike;

            return result;
        }

        public async Task<PaginationResponse<ReviewResponse>> GetByProductSlugAsync(ReviewForProductRequest request, string userId)
        {
            var productSpec = new ProductGetByUrlSlugSpecification(request.ProductSlug);
            var product = await _productRepository.GetBySpecAsync(productSpec);
            product.ProductNullChecking();

            var reviewSpec = new ReviewsByProductIdSpecification(product.Id);
            var count = await _reviewRepository.CountAsync(reviewSpec);

            var result = new PaginationResponse<ReviewResponse>() { Count = count };

            reviewSpec = new ReviewsByProductIdSpecification(
                product.Id,
                (request.Page - 1) * request.RowsPerPage,
                    request.RowsPerPage);
            var reviews = await _reviewRepository.ListAsync(reviewSpec);

            result.Values = _mapper.Map<IEnumerable<ReviewResponse>>(reviews);
            if (!string.IsNullOrEmpty(userId))
            {
                foreach (var item in result.Values)
                {
                    var spec = new ReviewVotesGetUserVoteSpecification(item.Id, userId);
                    var vote = await _reviewVotesRepository.GetBySpecAsync(spec);
                    item.IsLiked = vote != null && vote.IsLike;
                    item.IsDisliked = vote != null && !vote.IsLike;
                }
            }

            return result;
        }

        public async Task Like(int id, string userId)
        {
            var reviewSpec = new ReviewGetByIdSpecification(id);
            var review = await _reviewRepository.GetBySpecAsync(reviewSpec);
            review.ReviewNullChecking();

            var user = await _userManager.FindByIdAsync(userId);
            user.UserNullChecking();

            var spec = new ReviewVotesGetUserVoteSpecification(id, userId);
            var vote = await _reviewVotesRepository.GetBySpecAsync(spec);
            if (vote == null)
            {
                vote = new ReviewVotes() { ReviewId = id, UserId = userId, IsLike = true };
                await _reviewVotesRepository.AddAsync(vote);
            }
            else if (!vote.IsLike)
            {
                vote.IsLike = true;
                await _reviewVotesRepository.UpdateAsync(vote);
            }
            else
                await _reviewVotesRepository.DeleteAsync(vote);

            await _reviewVotesRepository.SaveChangesAsync();
        }

        public async Task Dislike(int id, string userId)
        {
            var reviewSpec = new ReviewGetByIdSpecification(id);
            var review = await _reviewRepository.GetBySpecAsync(reviewSpec);
            review.ReviewNullChecking();

            var user = await _userManager.FindByIdAsync(userId);
            user.UserNullChecking();

            var spec = new ReviewVotesGetUserVoteSpecification(id, userId);
            var vote = await _reviewVotesRepository.GetBySpecAsync(spec);
            if (vote == null)
            {
                vote = new ReviewVotes() { ReviewId = id, UserId = userId };
                await _reviewVotesRepository.AddAsync(vote);
            }
            else if (vote.IsLike)
            {
                vote.IsLike = false;
                await _reviewVotesRepository.UpdateAsync(vote);
            }
            else
                await _reviewVotesRepository.DeleteAsync(vote);

            await _reviewVotesRepository.SaveChangesAsync();
        }
    }
}
