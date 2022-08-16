using FluentValidation;
using Microsoft.Extensions.Localization;

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
        private readonly IStringLocalizer<ValidationResourсes> _validationResources;
        public EmailRequestValidator(IStringLocalizer<ValidationResourсes> validationResources)
        {
            _validationResources = validationResources;

            //Email
            RuleFor(x => x.Email).Cascade(CascadeMode.Stop)
                .NotEmpty().WithName(_validationResources["EmailAddressPropName"]).WithMessage(_validationResources["RequiredMessage"])
                .EmailAddress();
        }
    }
}
