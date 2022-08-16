using FluentValidation;
using Microsoft.Extensions.Localization;

namespace WebAPI.ViewModels.Request.Users
{
    /// <summary>
    /// Class for user sign in operation
    /// </summary>
    public class SignInRequest
    {
        /// <summary>
        /// User email address or phone number
        /// </summary>
        /// <example>email@gmail.com</example>
        public string EmailOrPhone { get; set; }
        /// <summary>
        /// User password
        /// </summary>
        /// <example>QWE_rty123</example>
        public string Password { get; set; }
        /// <summary>
        /// ReCaptcha token
        /// </summary>
        /// <example>some_reCaptcha_token</example>
        public string ReCaptchaToken { get; set; }
    }

    /// <summary>
    /// Class for <seealso cref="SignInRequest" /> validation
    /// </summary>
    public class SignInRequestValidator : AbstractValidator<SignInRequest>
    {
        private readonly IStringLocalizer<ValidationResourсes> _validationResources;
        public SignInRequestValidator(IStringLocalizer<ValidationResourсes> validationResources)
        {
            _validationResources = validationResources;

            //Email or phone
            RuleFor(x => x.EmailOrPhone).Cascade(CascadeMode.Stop)
                   .NotEmpty().WithName(_validationResources["EmailOrPhonePropName"]);

            //Password
            RuleFor(x => x.Password)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithName(_validationResources["PasswordPropName"]);
        }
    }
}
