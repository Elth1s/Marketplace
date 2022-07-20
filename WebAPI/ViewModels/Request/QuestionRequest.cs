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
        /// UserId 
        /// </summary>
        /// <example></example>
        public string UserId { get; set; }

        /// <summary>
        /// Product 
        /// </summary>
        /// <example>1</example>
        public int ProductId { get; set; }

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
               .Length(2, 30).WithMessage("{PropertyFullName} should be between 2 and 30 characters");

            //Message
            RuleFor(x => x.Message).Cascade(CascadeMode.Stop)
               .NotEmpty().WithName("Message").WithMessage("{PropertyMessage} is required")
               .Length(2,int.MaxValue).WithMessage("{PropertyMessage} should be min 2 characters");
        }
    }

}
