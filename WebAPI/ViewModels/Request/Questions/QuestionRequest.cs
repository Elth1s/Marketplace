using FluentValidation;

namespace WebAPI.ViewModels.Request.Questions
{
    /// <summary>
    /// Question class to create question 
    /// </summary>
    public class QuestionRequest
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
        /// Message 
        /// </summary>
        /// <example>Some information</example>

        public string Message { get; set; }

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
    /// Class for <seealso cref="QuestionRequest" /> validation
    /// </summary>
    public class QuestionRequestValidator : AbstractValidator<QuestionRequest>
    {
        public QuestionRequestValidator()
        {
            //FullName
            RuleFor(x => x.FullName).Cascade(CascadeMode.Stop)
               .NotEmpty().WithName("FullName").WithMessage("{PropertyName} is required")
               .Length(2, 80).WithMessage("{PropertyName} should be between 2 and 80 characters");

            //Message
            RuleFor(x => x.Message).Cascade(CascadeMode.Stop)
               .NotEmpty().WithName("Message").WithMessage("{PropertyName} is required")
               .Length(2, 500).WithMessage("{PropertyName} should be between 2 and 500 characters");

            //Email
            RuleFor(x => x.Email).Cascade(CascadeMode.Stop)
               .NotEmpty().WithName("Email address").WithMessage("{PropertyName} is required")
                .EmailAddress().WithMessage("Invalid format of {PropertyName}");
        }
    }

}
