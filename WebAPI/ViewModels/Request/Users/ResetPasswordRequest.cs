using FluentValidation;
using Microsoft.Extensions.Localization;

namespace WebAPI.ViewModels.Request.Users
{
    /// <summary>
    /// Reset password class to reset password for user
    /// </summary>
    public class ResetPasswordRequest
    {
        /// <summary>
        /// User identifier
        /// </summary>
        /// <example>94ddd073</example>
        public string UserId { get; set; }
        /// <summary>
        /// Token for reset password
        /// </summary>
        /// <example>some_reset_token</example>
        public string Token { get; set; }
        /// <summary>
        /// New password
        /// </summary>
        /// <example>QWE_rty123</example>
        public string Password { get; set; }
        /// <summary>
        /// Confirm password
        /// </summary>
        /// <example>QWE_rty123</example>
        public string ConfirmPassword { get; set; }
    }

    /// <summary>
    /// Class for <seealso cref="ResetPasswordRequest" /> validation
    /// </summary>
    public class ResetPasswordRequestValidator : AbstractValidator<ResetPasswordRequest>
    {
        private readonly IStringLocalizer<ValidationResourсes> _validationResources;
        public ResetPasswordRequestValidator(IStringLocalizer<ValidationResourсes> validationResources)
        {
            _validationResources = validationResources;
            //Password
            RuleFor(x => x.Password).Cascade(CascadeMode.Stop)
               .NotEmpty().WithName(_validationResources["PasswordPropName"])
               .MinimumLength(8).WithMessage(_validationResources["PasswordMinLengthMessage"])
               .Matches(@"(?=.*[A-Z])").WithMessage(_validationResources["ContainLowercaseMessage"])
               .Matches(@"(?=.*[A-Z])").WithMessage(_validationResources["ContainUppercaseMessage"])
               .Matches(@"(?=.*?[0-9])").WithMessage(_validationResources["ContainDigitMessage"])
               .Matches(@"(?=.*?[!@#\$&*~_-])").WithMessage(_validationResources["ContainSpecialCharacterMessage"]);


            //ConfirmPassword
            RuleFor(x => x.ConfirmPassword).Cascade(CascadeMode.Stop)
               .NotEmpty().WithName(_validationResources["ConfirmPasswordPropName"])
               .Equal(x => x.Password).WithMessage(_validationResources["PasswordConfrimPasswordEqualMessage"]);
        }
    }
}
