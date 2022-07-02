using FluentValidation;
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
        private readonly PhoneNumberManager _phoneNumberManager;
        public CodeRequestValidator(PhoneNumberManager phoneNumberManager)
        {
            _phoneNumberManager = phoneNumberManager;

            //Phone
            RuleFor(x => x.Phone).Cascade(CascadeMode.Stop)
               .NotEmpty().WithName("Phone number").WithMessage("{PropertyName} is required")
               .Must(IsValidPhone).WithMessage("Invalid {PropertyName} format");

            //Code
            RuleFor(x => x.Code).Cascade(CascadeMode.Stop)
                .NotEmpty().WithName("Code").WithMessage("{PropertyName} is required")
                .Length(6).WithMessage("{PropertyName} must be 6 characters long");
        }

        private bool IsValidPhone(string phone)
        {
            return _phoneNumberManager.IsValidNumber(phone);
        }
    }
}
