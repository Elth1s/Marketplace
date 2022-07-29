using FluentValidation;

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
        /// Product advantage
        /// </summary>
        /// <example>Some list of benefits</example>
        public string Advantage { get; set; }
        /// <summary>
        /// Product disadvantage
        /// </summary>
        /// <example>Some list of disadvantages</example>
        public string Disadvantage { get; set; }
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
        public ICollection<int> Images { get; set; }
    }

    /// <summary>
    /// Class for <seealso cref="ReviewRequest" /> validation
    /// </summary>
    public class ReviewRequestValidator : AbstractValidator<ReviewRequest>
    {
        public ReviewRequestValidator()
        {
            //Full name
            RuleFor(x => x.FullName).Cascade(CascadeMode.Stop)
               .NotEmpty().WithName("Full name").WithMessage("{PropertyName} is required")
               .Length(2, 80).WithMessage("{PropertyName} should be between 2 and 60 characters");

            //Email
            RuleFor(x => x.Email).Cascade(CascadeMode.Stop)
              .NotEmpty().WithName("Email address").WithMessage("{PropertyName} is required")
              .EmailAddress().WithMessage("Invalid format of {PropertyName}"); ;

            //Product rating
            RuleFor(c => c.ProductRating).Cascade(CascadeMode.Stop)
               .NotEmpty().WithName("Product rating").WithMessage("{PropertyName} is required!")
               .InclusiveBetween(1, 5).WithMessage("{PropertyName} should be between 1 and  5");

            //Advantage
            RuleFor(a => a.Advantage).Cascade(CascadeMode.Stop)
               .NotEmpty().WithName("Advantage").WithMessage("{PropertyName} is required!")
               .MaximumLength(100).WithMessage("{PropertyName} should be less than 100 characters");

            //Disadvantage
            RuleFor(a => a.Disadvantage).Cascade(CascadeMode.Stop)
               .NotEmpty().WithName("Disadvantage").WithMessage("{PropertyName} is required!")
               .MaximumLength(100).WithMessage("{PropertyName} should be less than 100 characters");

            //Comment
            RuleFor(a => a.Comment).Cascade(CascadeMode.Stop)
               .NotEmpty().WithName("Comment").WithMessage("{PropertyName} is required!")
               .Length(1, 850).WithMessage("{PropertyName} should be between 1 and 850 characters");

            //Full name
            RuleFor(x => x.ProductSlug).Cascade(CascadeMode.Stop)
               .NotEmpty().WithName("Product slug").WithMessage("{PropertyName} is required")
               .Length(2, 70).WithMessage("{PropertyName} should be between 2 and 70 characters");
        }
    }
}
