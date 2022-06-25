using FluentValidation;

namespace WebAPI.ViewModels.Request.Users
{
    /// <summary>
    /// Email class for sending email
    /// </summary>
    public class EmailRequest
    {
        /// <summary>
        /// User email address
        /// </summary>
        /// <example>email@gmail.com</example>
        public string Email { get; set; }
    }

    /// <summary>
    /// Class for <seealso cref="EmailRequest" /> validation
    /// </summary>
    public class EmailRequestValidator : AbstractValidator<EmailRequest>
    {
        public EmailRequestValidator()
        {
            RuleFor(x => x.Email).Cascade(CascadeMode.Stop)
                .NotEmpty().WithName("Email address").WithMessage("{PropertyName} is required")
                .EmailAddress().WithMessage("Invalid format of {PropertyName}"); ;
        }
    }
}
