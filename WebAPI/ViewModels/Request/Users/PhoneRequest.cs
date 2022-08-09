using FluentValidation;
using Microsoft.Extensions.Localization;
using WebAPI.Helpers;

namespace WebAPI.ViewModels.Request
{
    /// <summary>
    /// Phone class for sending phone code
    /// </summary>
    public class PhoneRequest
    {
        /// <summary>
        /// User phone number
        /// </summary>
        /// <example>+380 50 638 8216</example>
        public string Phone { get; set; }
    }

    /// <summary>
    /// Class for <seealso cref="PhoneRequest" /> validation
    /// </summary>
    public class PhoneRequestValidator : AbstractValidator<PhoneRequest>
    {
        private readonly IStringLocalizer<ValidationResourсes> _validationResources;
        private readonly PhoneNumberManager _phoneNumberManager;
        public PhoneRequestValidator(PhoneNumberManager phoneNumberManager, IStringLocalizer<ValidationResourсes> validationResources)
        {
            _validationResources = validationResources;
            _phoneNumberManager = phoneNumberManager;

            //Phone
            RuleFor(x => x.Phone).Cascade(CascadeMode.Stop)
               .NotEmpty().WithName(_validationResources["PhonePropName"])
               .Must(IsValidPhone).WithMessage(_validationResources["InvalidFormatMessage"]);
        }

        private bool IsValidPhone(string phone)
        {
            return _phoneNumberManager.IsValidNumber(phone);
        }
    }
}
