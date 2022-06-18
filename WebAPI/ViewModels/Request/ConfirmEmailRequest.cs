using FluentValidation;

namespace WebAPI.ViewModels.Request
{
    /// <summary>
    /// Confirm email class to verify email address
    /// </summary>
    public class ConfirmEmailRequest
    {
        /// <summary>
        /// User identifier
        /// </summary>
        /// <example>94ddd073</example>
        public string UserId { get; set; }
        /// <summary>
        /// Confirmation code
        /// </summary>
        /// <example>some_confirmation_code</example>
        public string ConfirmationCode { get; set; }
    }

    /// <summary>
    /// Class for <seealso cref="ConfirmEmailRequest" /> validation
    /// </summary>
    public class ConfirmEmailRequestValidator : AbstractValidator<ConfirmEmailRequest>
    {
        public ConfirmEmailRequestValidator()
        {
            //User id
            RuleFor(x => x.UserId).NotEmpty().WithName("User id").WithMessage("{PropertyName} is required");

            //Confirmation code
            RuleFor(x => x.ConfirmationCode).NotEmpty().WithName("Confirmation code").WithMessage("{PropertyName} is required");
        }
    }
}
