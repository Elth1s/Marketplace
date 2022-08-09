using DAL.Entities;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
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
        private readonly IStringLocalizer<ValidationResourсes> _validationResources;
        private bool IsPhone = false;
        private bool IsEmail = false;

        public SignUpRequestValidator(UserManager<AppUser> userManager, PhoneNumberManager phoneNumberManager,
            IStringLocalizer<ValidationResourсes> validationResources)
        {
            _userManager = userManager;
            _phoneNumberManager = phoneNumberManager;
            _validationResources = validationResources;

            //First name
            RuleFor(x => x.FirstName).Cascade(CascadeMode.Stop)
               .NotEmpty().WithName(_validationResources["FirstNamePropName"])
               .Length(2, 15);

            //Second name
            RuleFor(x => x.SecondName).Cascade(CascadeMode.Stop)
              .NotEmpty().WithName(_validationResources["SecondNamePropName"])
              .Length(2, 40);

            //Email or phone number
            RuleFor(x => x.EmailOrPhone).Cascade(CascadeMode.Stop)
               .NotEmpty().WithName(_validationResources["EmailOrPhonePropName"])
               .Must(IsEmailOrPhoneValid).WithMessage(_validationResources["InvalidFormatMessage"])
               .Must(IsUniqueEmail).WithMessage(_validationResources["UserUniqueEmailMessage"])
               .Must(IsUniquePhone).WithMessage(_validationResources["UserUniquePhoneMessage"]);

            //Password
            RuleFor(x => x.Password).Cascade(CascadeMode.Stop)
               .NotEmpty().WithName(_validationResources["PasswordPropName"])
               .MinimumLength(8).WithMessage(_validationResources["PasswordMinLengthMessage"])
               .Matches(@"(?=.*[A-Z])").WithMessage(_validationResources["ContainLowercaseMessage"])
               .Matches(@"(?=.*[A-Z])").WithMessage(_validationResources["ContainUppercaseMessage"])
               .Matches(@"(?=.*?[0-9])").WithMessage(_validationResources["ContainDigitMessage"])
               .Matches(@"(?=.*?[!@#\$&*~_-])").WithMessage(_validationResources["ContainSpecialCharacterMessage"]);

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
