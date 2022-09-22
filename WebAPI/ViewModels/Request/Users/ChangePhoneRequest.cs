using DAL.Entities;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using WebAPI.Extensions;
using WebAPI.Helpers;

namespace WebAPI.ViewModels.Request.Users
{
    /// <summary>
    /// Change phone class to change phone for user
    /// </summary>
    public class ChangePhoneRequest
    {
        /// <summary>
        /// User phone number
        /// </summary>
        /// <example>+380 50 638 82 16</example>
        public string Phone { get; set; }
        /// <summary>
        /// User password
        /// </summary>
        /// <example>QWE_rty123</example>
        public string Password { get; set; }
    }

    /// <summary>
    /// Class for <seealso cref="ChangePhoneRequest" /> validation
    /// </summary>
    public class ChangePhoneRequestValidator : AbstractValidator<ChangePhoneRequest>
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly PhoneNumberManager _phoneNumberManager;
        private readonly IStringLocalizer<ValidationResourсes> _validationResources;
        public ChangePhoneRequestValidator(UserManager<AppUser> userManager, PhoneNumberManager phoneNumberManager,
            IStringLocalizer<ValidationResourсes> validationResources)
        {
            _validationResources = validationResources;
            _userManager = userManager;
            _phoneNumberManager = phoneNumberManager;

            //Phone
            RuleFor(x => x.Phone).Cascade(CascadeMode.Stop)
               .NotEmpty().WithName(_validationResources["PhonePropName"])
               .Must(IsValidPhone).WithMessage(_validationResources["InvalidFormatMessage"])
               .Must(IsUniquePhone).WithMessage(_validationResources["UserUniquePhoneMessage"]);


            //Password
            RuleFor(x => x.Password)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithName(_validationResources["PasswordPropName"]);
        }

        private bool IsUniquePhone(string phone)
        {
            var formatedPhone = _phoneNumberManager.GetPhoneE164Format(phone);
            return _userManager.GetByPhoneNumberAsync(formatedPhone).Result == null;
        }
        private bool IsValidPhone(string phone)
        {
            return _phoneNumberManager.IsValidNumber(phone);
        }
    }
}
