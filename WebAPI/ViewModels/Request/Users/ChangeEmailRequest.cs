using DAL.Entities.Identity;
using FluentValidation;
using Microsoft.AspNetCore.Identity;

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
        public ChangeEmailRequestValidator(UserManager<AppUser> userManager)
        {
            _userManager = userManager;

            //Email
            RuleFor(x => x.Email).Cascade(CascadeMode.Stop)
               .NotEmpty().WithName("Email address").WithMessage("{PropertyName} is required")
               .EmailAddress().WithMessage("Invalid format of {PropertyName}")
               .Must(IsUniqueEmail).WithMessage("User with this {PropertyName} already exists");

            //Password
            RuleFor(x => x.Password).Cascade(CascadeMode.Stop)
                .NotEmpty().WithName("Password").WithMessage("{PropertyName} is required");
        }

        private bool IsUniqueEmail(string email)
        {
            return _userManager.FindByEmailAsync(email).Result == null;
        }
    }
}
