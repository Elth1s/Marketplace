using FluentValidation;
using Microsoft.Extensions.Localization;

namespace WebAPI.ViewModels.Request.Reviews
{
    /// <summary>
    /// Review class to create review 
    /// </summary>
    public class ReviewRequest
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
        /// Product rating
        /// </summary>
        /// <example>4</example>
        public float ProductRating { get; set; }
        /// <summary>
        /// Product advantages
        /// </summary>
        /// <example>Some list of benefits</example>
        public string Advantages { get; set; }
        /// <summary>
        /// Product disadvantages
        /// </summary>
        /// <example>Some list of disadvantages</example>
        public string Disadvantages { get; set; }
        /// <summary>
        /// Comment
        /// </summary>
        /// <example>Some information</example>
        public string Comment { get; set; }
        /// <summary>
        /// Video URL
        /// </summary>
        /// <example>https://some_video_url_example.jpg</example>
        public string VideoURL { get; set; }
        /// <summary>
        /// Product slug
        /// </summary>
        /// <example>some-product-slug</example>
        public string ProductSlug { get; set; }
        /// <summary>
        /// List of images
        /// </summary>
        public ICollection<string> Images { get; set; }
    }

    /// <summary>
    /// Class for <seealso cref="ReviewRequest" /> validation
    /// </summary>
    public class ReviewRequestValidator : AbstractValidator<ReviewRequest>
    {
        private readonly IStringLocalizer<ValidationResourсes> _validationResources;
        public ReviewRequestValidator(IStringLocalizer<ValidationResourсes> validationResources)
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

            //Product rating
            RuleFor(c => c.ProductRating).Cascade(CascadeMode.Stop)
               .NotEmpty().WithName(_validationResources["ProductRatingPropName"])
               .InclusiveBetween(0.5f, 5);

            //Advantage
            RuleFor(a => a.Advantages).Cascade(CascadeMode.Stop)
               .MaximumLength(100).WithName(_validationResources["AdvantagesPropName"]).WithMessage(_validationResources["PluralLengthMessage"]);

            //Disadvantage
            RuleFor(a => a.Disadvantages).Cascade(CascadeMode.Stop)
               .MaximumLength(100).WithName(_validationResources["DisadvantagesPropName"]).WithMessage(_validationResources["PluralLengthMessage"]);

            //Comment
            RuleFor(a => a.Comment).Cascade(CascadeMode.Stop)
               .NotEmpty().WithName(_validationResources["CommentPropName"])
               .Length(1, 850);

            //Product slug 
            RuleFor(a => a.ProductSlug).Cascade(CascadeMode.Stop)
                 .NotEmpty().WithName(_validationResources["ProductUrlSlugPropName"])
                 .WithMessage(_validationResources["RequiredMessage"]);
        }
    }
}
