using FluentValidation;
using Microsoft.Extensions.Localization;

namespace WebAPI.ViewModels.Request.Users
{
    /// <summary>
    /// Add password class to add password for user
    /// </summary>
    public class AddPasswordRequest
    {
        /// <summary>
        /// User password
        /// </summary>
        /// <example>QWE_rty123</example>
        public string Password { get; set; }
        /// <summary>
        /// User confirm password
        /// </summary>
        /// <example>QWE_rty123</example>
        public string ConfirmPassword { get; set; }
    }

    /// <summary>
    /// Class for <seealso cref="AddPasswordRequest" /> validation
    /// </summary>
    public class AddPasswordRequestValidator : AbstractValidator<AddPasswordRequest>
    {
        private readonly IStringLocalizer<ValidationResourсes> _validationResources;
        public AddPasswordRequestValidator(IStringLocalizer<ValidationResourсes> validationResources)
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
