using FluentValidation;
using Microsoft.Extensions.Localization;

namespace WebAPI.ViewModels.Request.Users
{
    /// <summary>
    /// Change password class to change password for user
    /// </summary>
    public class ChangePasswordRequest
    {
        /// <summary>
        /// User old password
        /// </summary>
        /// <example>QWE_rty123</example>
        public string OldPassword { get; set; }
        /// <summary>
        /// User new password
        /// </summary>
        /// <example>321ytr_EWQ</example>
        public string Password { get; set; }
        /// <summary>
        /// User confirm new password
        /// </summary>
        /// <example>321ytr_EWQ</example>
        public string ConfirmPassword { get; set; }
    }

    /// <summary>
    /// Class for <seealso cref="ChangePasswordRequest" /> validation
    /// </summary>
    public class ChangePasswordRequestValidator : AbstractValidator<ChangePasswordRequest>
    {
        private readonly IStringLocalizer<ValidationResourсes> _validationResources;
        public ChangePasswordRequestValidator(IStringLocalizer<ValidationResourсes> validationResources)
        {
            _validationResources = validationResources;

            //OldPassword
            RuleFor(x => x.OldPassword).Cascade(CascadeMode.Stop)
           .NotEmpty().WithName(_validationResources["OldPasswordPropName"]);

            //Password
            RuleFor(x => x.Password).Cascade(CascadeMode.Stop)
               .NotEmpty().WithName(_validationResources["PasswordPropName"])
               .MinimumLength(8).WithMessage(_validationResources["PasswordMinLengthMessage"])
               .Matches(@"(?=.*[A-Z])").WithMessage(_validationResources["ContainLowercaseMessage"])
               .Matches(@"(?=.*[A-Z])").WithMessage(_validationResources["ContainUppercaseMessage"])
               .Matches(@"(?=.*?[0-9])").WithMessage(_validationResources["ContainDigitMessage"])
               .Matches(@"(?=.*?[!@#\$&*~_-])").WithMessage(_validationResources["ContainSpecialCharacterMessage"]);

            //Confirm Password
            RuleFor(x => x.ConfirmPassword).Cascade(CascadeMode.Stop)
               .NotEmpty().WithName(_validationResources["ConfirmPasswordPropName"])
               .Equal(x => x.Password).WithMessage(_validationResources["PasswordConfrimPasswordEqualMessage"]);
        }
    }

}
