using FluentValidation;

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
        public ConfirmPhoneRequestValidator()
        {
            //Code
            RuleFor(x => x.Code).Cascade(CascadeMode.Stop)
                .NotEmpty().WithName("Code").WithMessage("{PropertyName} is required")
                .Length(6).WithMessage("{PropertyName} must be 6 characters long");
        }
    }
}
