using FluentValidation;
using Microsoft.Extensions.Localization;

namespace WebAPI.ViewModels.Request.Users
{
    /// <summary>
    /// Class for external user login 
    /// </summary>
    public class ExternalLoginRequest
    {
        /// <summary>
        /// External user login token
        /// </summary>
        /// <example>some_external_login_token</example>
        public string Token { get; set; }
    }


    /// <summary>
    /// Class for <seealso cref="ExternalLoginRequest" /> validation
    /// </summary>
    public class ExternalLoginRequestValidator : AbstractValidator<ExternalLoginRequest>
    {
        private readonly IStringLocalizer<ValidationResourсes> _validationResources;
        public ExternalLoginRequestValidator(IStringLocalizer<ValidationResourсes> validationResources)
        {
            _validationResources = validationResources;

            RuleFor(x => x.Token).Cascade(CascadeMode.Stop)
                .NotEmpty().WithName(_validationResources["TokenPropName"]);
        }
    }
}
