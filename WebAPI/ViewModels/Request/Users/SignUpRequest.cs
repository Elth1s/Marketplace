using DAL.Entities.Identity;
using FluentValidation;
using Microsoft.AspNetCore.Identity;

namespace WebAPI.ViewModels.Request.Users
{
    /// <summary>
    /// Class for user sign up operation
    /// </summary>
    public class SignUpRequest
    {
        /// <summary>
        /// User first name
        /// </summary>
        /// <example>Nick</example>
        public string FirstName { get; set; }
        /// <summary>
        /// User second name
        /// </summary>
        /// <example>Smith</example>
        public string SecondName { get; set; }
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
    /// Class for <seealso cref="SignUpRequest" /> validation
    /// </summary>
    public class SignUpRequestValidator : AbstractValidator<SignUpRequest>
    {
        private readonly UserManager<AppUser> _userManager;
        public SignUpRequestValidator(UserManager<AppUser> userManager)
        {
            _userManager = userManager;

            //First name
            RuleFor(x => x.FirstName).Cascade(CascadeMode.Stop)
               .NotEmpty().WithName("First name").WithMessage("{PropertyName} is required")
               .Length(2, 15).WithMessage("{PropertyName} should be between 2 and 15 characters");

            //Second name
            RuleFor(x => x.SecondName).Cascade(CascadeMode.Stop)
              .NotEmpty().WithName("Second name").WithMessage("{PropertyName} is required")
              .Length(2, 40).WithMessage("{PropertyName} should be between 2 and 40 characters");

            //Email
            RuleFor(x => x.Email).Cascade(CascadeMode.Stop)
               .NotEmpty().WithName("Email address").WithMessage("{PropertyName} is required")
               .EmailAddress().WithMessage("Invalid format of {PropertyName}")
               .Must(IsUniqueEmail).WithMessage("User with this {PropertyName} already exists");

            //Password
            RuleFor(x => x.Password).Cascade(CascadeMode.Stop)
           .NotEmpty().WithMessage("{PropertyName} is required")
           .MinimumLength(8).WithMessage("{PropertyName} must be at least 8 characters")
           .Matches(@"(?=.*[A-Z])").WithMessage("{PropertyName} must contain at least one lowercase letter")
           .Matches(@"(?=.*[A-Z])").WithMessage("{PropertyName} must contain at least one uppercase letter")
           .Matches(@"(?=.*?[0-9])").WithMessage("{PropertyName} must contain at least one digit")
           .Matches(@"(?=.*?[!@#\$&*~_-])").WithMessage("{PropertyName} must contain at least one special character");

        }
        private bool IsUniqueEmail(string email)
        {
            return _userManager.FindByEmailAsync(email).Result == null;
        }
    }
}
