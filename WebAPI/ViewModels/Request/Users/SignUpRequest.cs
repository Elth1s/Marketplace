using DAL.Entities;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using WebAPI.Extensions;
using WebAPI.Helpers;

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
    /// Class for <seealso cref="SignUpRequest" /> validation
    /// </summary>
    public class SignUpRequestValidator : AbstractValidator<SignUpRequest>
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly PhoneNumberManager _phoneNumberManager;
        private bool IsPhone = false;
        private bool IsEmail = false;

        public SignUpRequestValidator(UserManager<AppUser> userManager, PhoneNumberManager phoneNumberManager)
        {
            _userManager = userManager;
            _phoneNumberManager = phoneNumberManager;

            //First name
            RuleFor(x => x.FirstName).Cascade(CascadeMode.Stop)
               .NotEmpty().WithName("First name").WithMessage("{PropertyName} is required")
               .Length(2, 15).WithMessage("{PropertyName} should be between 2 and 15 characters");

            //Second name
            RuleFor(x => x.SecondName).Cascade(CascadeMode.Stop)
              .NotEmpty().WithName("Second name").WithMessage("{PropertyName} is required")
              .Length(2, 40).WithMessage("{PropertyName} should be between 2 and 40 characters");

            //Email or phone number
            RuleFor(x => x.EmailOrPhone).Cascade(CascadeMode.Stop)
               .NotEmpty().WithName("Email address or phone").WithMessage("{PropertyName} is required")
               .Must(IsEmailOrPhoneValid).WithMessage("Invalid format of {PropertyName}")
               .Must(IsUniqueEmail).WithMessage("User with this Email address already exists")
               .Must(IsUniquePhone).WithMessage("User with this Phone already exists");

            //Password
            RuleFor(x => x.Password).Cascade(CascadeMode.Stop)
               .NotEmpty().WithMessage("{PropertyName} is required")
               .MinimumLength(8).WithMessage("{PropertyName} must be at least 8 characters")
               .Matches(@"(?=.*[A-Z])").WithMessage("{PropertyName} must contain at least one lowercase letter")
               .Matches(@"(?=.*[A-Z])").WithMessage("{PropertyName} must contain at least one uppercase letter")
               .Matches(@"(?=.*?[0-9])").WithMessage("{PropertyName} must contain at least one digit")
               .Matches(@"(?=.*?[!@#\$&*~_-])").WithMessage("{PropertyName} must contain at least one special character");

        }
        private bool IsValidEmail(string email)
        {
            IsEmail = EmailManager.IsValidEmail(email);
            return IsEmail;
        }
        private bool IsValidPhone(string phone)
        {
            IsPhone = _phoneNumberManager.IsValidNumber(phone);
            return IsPhone;
        }
        private bool IsEmailOrPhoneValid(string data)
        {
            return IsValidEmail(data) || IsValidPhone(data); ;
        }
        private bool IsUniqueEmail(string email)
        {
            if (!IsEmail)
                return true;

            return _userManager.FindByEmailAsync(email).Result == null;
        }
        private bool IsUniquePhone(string phone)
        {
            if (!IsPhone)
                return true;

            var formatedPhone = _phoneNumberManager.GetPhoneE164Format(phone);
            return _userManager.FindByPhoneNumberAsync(formatedPhone).Result == null;
        }
    }
}
