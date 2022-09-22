using DAL.Entities;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using WebAPI.Extensions;

namespace WebAPI.ViewModels.Request.Users
{
    /// <summary>
    /// Change email class to change email for user
    /// </summary>
    public class ChangeEmailRequest
    {
        /// <summary>
        /// User email
        /// </summary>
        /// <example>email@gmail.com</example>
        public string Email { get; set; }
        /// <summary>
        /// User password
        /// </summary>
        /// <example>QWE_rty123</example>
        public string Password { get; set; }
    }

    /// <summary>
    /// Class for <seealso cref="ChangeEmailRequest" /> validation
    /// </summary>
    public class ChangeEmailRequestValidator : AbstractValidator<ChangeEmailRequest>
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IStringLocalizer<ValidationResourсes> _validationResources;
        public ChangeEmailRequestValidator(UserManager<AppUser> userManager,
            IStringLocalizer<ValidationResourсes> validationResources)
        {
            _validationResources = validationResources;
            _userManager = userManager;

            //Email
            RuleFor(x => x.Email).Cascade(CascadeMode.Stop)
               .NotEmpty().WithName(_validationResources["EmailAddressPropName"]).WithMessage(_validationResources["RequiredMessage"])
               .EmailAddress()
               .Must(IsUniqueEmail).WithMessage(_validationResources["UserUniqueEmailMessage"]);

            //Password
            RuleFor(x => x.Password).Cascade(CascadeMode.Stop)
                .NotEmpty().WithName(_validationResources["PasswordPropName"]);
        }

        private bool IsUniqueEmail(string email)
        {
            return _userManager.GetByEmailAsync(email).Result == null;
        }
    }
}
