using FluentValidation;
using Microsoft.Extensions.Localization;

namespace WebAPI.ViewModels.Request.Shops
{
    /// <summary>
    /// Shop review class to create shop review 
    /// </summary>
    public class ShopReviewRequest
    {
        /// <summary>
        /// User full name
        /// </summary>
        /// <example>Nick Smith</example>
        public string FullName { get; set; }
        /// <summary>
        /// User email address
        /// </summary>
        /// <example>email@gmail.com</example>
        public string Email { get; set; }
        /// <summary>
        ///  Service quality rating
        /// </summary>
        /// <example>4</example>
        public int ServiceQualityRating { get; set; }
        /// <summary>
        /// Timeliness rating
        /// </summary>
        /// <example>4</example>
        public int TimelinessRating { get; set; }
        /// <summary>
        /// Information relevance rating
        /// </summary>
        /// <example>4</example>
        public int InformationRelevanceRating { get; set; }
        /// <summary>
        /// Comment
        /// </summary>
        /// <example>Some information</example>
        public string Comment { get; set; }
        /// <summary>
        /// Shop identifier
        /// </summary>
        /// <example>1</example>
        public int ShopId { get; set; }
    }

    /// <summary>
    /// Class for <seealso cref="ShopReviewRequest" /> validation
    /// </summary>
    public class ShopReviewRequestValidator : AbstractValidator<ShopReviewRequest>
    {
        private readonly IStringLocalizer<ValidationResourсes> _validationResources;
        public ShopReviewRequestValidator(IStringLocalizer<ValidationResourсes> validationResources)
        {
            _validationResources = validationResources;

            //Full name
            RuleFor(x => x.FullName).Cascade(CascadeMode.Stop)
               .NotEmpty().WithName(_validationResources["FullNamePropName"]).WithMessage(_validationResources["PluralRequiredMessage"])
               .Length(2, 80).WithMessage(_validationResources["PluralLengthMessage"]);

            //Email
            RuleFor(x => x.Email).Cascade(CascadeMode.Stop)
              .NotEmpty().WithName(_validationResources["EmailAddressPropName"]).WithMessage(_validationResources["RequiredMessage"])
              .EmailAddress();

            //Service Quality Rating
            RuleFor(c => c.ServiceQualityRating).Cascade(CascadeMode.Stop)
               .NotEmpty().WithName(_validationResources["ServiceQualityRatingPropName"])
               .InclusiveBetween(1, 5);

            //Timeliness Rating
            RuleFor(c => c.TimelinessRating).Cascade(CascadeMode.Stop)
               .NotEmpty().WithName(_validationResources["TimelinessRatingPropName"])
               .InclusiveBetween(1, 5);

            //Information Relevance Rating
            RuleFor(c => c.InformationRelevanceRating).Cascade(CascadeMode.Stop)
               .NotEmpty().WithName(_validationResources["InformationRelevanceRatingPropName"])
               .InclusiveBetween(1, 5);

            //Comment
            RuleFor(a => a.Comment).Cascade(CascadeMode.Stop)
               .NotEmpty().WithName(_validationResources["CommentPropName"])
               .Length(1, 450);
        }
    }
}
