using FluentValidation;

namespace WebAPI.ViewModels.Request
{
    public class QuestionRequest
    {
        /// <summary>
        /// FullName 
        /// </summary>
        /// <example>Novak Vova</example>
        public string FullName { get; set; }
        /// <summary>
        /// Email 
        /// </summary>
        /// <example>vova@gmail.com</example>

        public string Email { get; set; }
        /// <summary>
        /// Message 
        /// </summary>
        /// <example>Lorem Ipsum Dolor</example>

        public string Message { get; set; }

        /// <summary>
        /// Product url slug
        /// </summary>
        /// <example>qweqdqdq-qweqqdqd-qweq</example>
        public int ProductSlug { get; set; }

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
               .NotEmpty().WithName("FullName").WithMessage("{PropertyFullName} is required")
               .Length(2, 80).WithMessage("{PropertyFullName} should be between 2 and 80 characters");

            //Message
            RuleFor(x => x.Message).Cascade(CascadeMode.Stop)
               .NotEmpty().WithName("Message").WithMessage("{PropertyMessage} is required")
               .Length(2,250).WithMessage("{PropertyMessage} should be between 2 and 250 characters");
            //Email
            RuleFor(x => x.Email).Cascade(CascadeMode.Stop)
               .NotEmpty().WithName("Email").WithMessage("{Email} is required").EmailAddress()
               .Length(11,1000).WithMessage("{Email} should be between 11 and 1000 characters");
        }
    }

}
