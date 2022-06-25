using FluentValidation;

namespace WebAPI.ViewModels.Request.Users
{
    /// <summary>
    /// Class for user sign in operation
    /// </summary>
    public class SignInRequest
    {
        /// <summary>
        /// User email address
        /// </summary>
        /// <example>email@gmail.com</example>
        public string Email { get; set; }
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
        public SignInRequestValidator()
        {
            //Email
            RuleFor(x => x.Email).Cascade(CascadeMode.Stop)
                   .NotEmpty().WithName("Email address").WithMessage("{PropertyName} is required")
                   .EmailAddress().WithMessage("Invalid format of {PropertyName}");

            //Password
            RuleFor(x => x.Password)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithName("Password").WithMessage("{PropertyName} is required");
        }
    }
}
