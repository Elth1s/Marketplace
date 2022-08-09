using FluentValidation;
using Microsoft.Extensions.Localization;
using WebAPI.Helpers;

namespace WebAPI.ViewModels.Request
{
    /// <summary>
    /// Phone code class to verify code
    /// </summary>
    public class CodeRequest
    {
        /// <summary>
        /// User phone number
        /// </summary>
        /// <example>+380 50 638 8216</example>
        public string Phone { get; set; }
        /// <summary>
        /// Confirmation code
        /// </summary>
        /// <example>123456</example>
        public string Code { get; set; }
    }

    /// <summary>
    /// Class for <seealso cref="CodeRequest" /> validation
    /// </summary>
    public class CodeRequestValidator : AbstractValidator<CodeRequest>
    {
        private readonly IStringLocalizer<ValidationResourсes> _validationResources;
        private readonly PhoneNumberManager _phoneNumberManager;
        public CodeRequestValidator(PhoneNumberManager phoneNumberManager,
            IStringLocalizer<ValidationResourсes> validationResources)
        {
            _validationResources = validationResources;
            _phoneNumberManager = phoneNumberManager;

            //Phone
            RuleFor(x => x.Phone).Cascade(CascadeMode.Stop)
               .NotEmpty().WithName(_validationResources["PhonePropName"])
               .Must(IsValidPhone).WithMessage(_validationResources["InvalidFormatMessage"]);

            //Code
            RuleFor(x => x.Code).Cascade(CascadeMode.Stop)
                .NotEmpty().WithName(_validationResources["CodePropName"])
                .Length(6);
        }

        private bool IsValidPhone(string phone)
        {
            return _phoneNumberManager.IsValidNumber(phone);
        }
    }
}
