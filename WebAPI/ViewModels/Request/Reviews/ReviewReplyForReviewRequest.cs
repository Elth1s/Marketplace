using FluentValidation;
using Microsoft.Extensions.Localization;

namespace WebAPI.ViewModels.Request.Reviews
{
    /// <summary>
    /// Review reply for review class to pagination review reply
    /// </summary>
    public class ReviewReplyForReviewRequest
    {
        /// <summary>
        /// Review identifier
        /// </summary>
        /// <example>1</example>
        public int ReviewId { get; set; }
        /// <summary>
        /// Page
        /// </summary>
        /// <example>1</example>
        public int Page { get; set; }

        /// <summary>
        /// Row per page
        /// </summary>
        /// <example>8</example>
        public int RowsPerPage { get; set; }
    }


    /// <summary>
    /// Class for <seealso cref="ReviewReplyForReviewRequest" /> validation
    /// </summary>
    public class ReviewReplyForReviewRequestValidation : AbstractValidator<ReviewReplyForReviewRequest>
    {
        private readonly IStringLocalizer<ValidationResourсes> _validationResources;
        public ReviewReplyForReviewRequestValidation(IStringLocalizer<ValidationResourсes> validationResources)
        {
            _validationResources = validationResources;

            //Page
            RuleFor(a => a.Page).Cascade(CascadeMode.Stop)
                .NotEmpty().WithName(_validationResources["PagePropName"])
                .WithMessage(_validationResources["RequiredMessage"])
                .GreaterThan(0).WithMessage(_validationResources["GreaterThanMessage"]);

            //RowsPerPage
            RuleFor(a => a.RowsPerPage).Cascade(CascadeMode.Stop)
                .NotEmpty().WithName(_validationResources["RowsPerPagePropName"])
                .WithMessage(_validationResources["PluralRequiredMessage"])
                .GreaterThan(0).WithMessage(_validationResources["PluralGreaterThanMessage"]);
        }
    }
}
