using FluentValidation;
using Microsoft.Extensions.Localization;

namespace WebAPI.ViewModels.Request.Users
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
        private readonly IStringLocalizer<ValidationResourсes> _validationResources;
        public ConfirmEmailRequestValidator(IStringLocalizer<ValidationResourсes> validationResources)
        {
            _validationResources = validationResources;

            //Confirmation code
            RuleFor(x => x.ConfirmationCode)
                .NotEmpty().WithName(_validationResources["ConfirmationCodePropName"]);
        }
    }
}
