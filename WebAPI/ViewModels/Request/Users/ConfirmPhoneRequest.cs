using FluentValidation;
using Microsoft.Extensions.Localization;

namespace WebAPI.ViewModels.Request.Users
{
    /// <summary>
    /// Confirm phone class to verify phone number
    /// </summary>
    public class ConfirmPhoneRequest
    {
        /// <summary>
        /// Confirmation code
        /// </summary>
        /// <example>123456</example>
        public string Code { get; set; }
    }

    /// <summary>
    /// Class for <seealso cref="ConfirmPhoneRequest" /> validation
    /// </summary>
    public class ConfirmPhoneRequestValidator : AbstractValidator<ConfirmPhoneRequest>
    {
        private readonly IStringLocalizer<ValidationResourсes> _validationResources;
        public ConfirmPhoneRequestValidator(IStringLocalizer<ValidationResourсes> validationResources)
        {
            _validationResources = validationResources;

            //Code
            RuleFor(x => x.Code).Cascade(CascadeMode.Stop)
                .NotEmpty().WithName(_validationResources["CodePropName"])
                .Length(6);
        }
    }
}
