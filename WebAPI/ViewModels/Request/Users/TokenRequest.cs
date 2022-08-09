using FluentValidation;
using Microsoft.Extensions.Localization;

namespace WebAPI.ViewModels.Request.Users
{
    /// <summary>
    /// Class for user operations with refresh token
    /// </summary>
    public class TokenRequest
    {
        /// <summary>
        /// Refresh token
        /// </summary>
        /// <example>some_refresh_token</example>
        public string Token { get; set; }
    }

    /// <summary>
    /// Class for <seealso cref="TokenRequest" /> validation
    /// </summary>
    public class TokenRequestValidator : AbstractValidator<TokenRequest>
    {
        private readonly IStringLocalizer<ValidationResourсes> _validationResources;
        public TokenRequestValidator(IStringLocalizer<ValidationResourсes> validationResources)
        {
            _validationResources = validationResources;

            RuleFor(x => x.Token).Cascade(CascadeMode.Stop)
                .NotEmpty().WithName(_validationResources["TokenPropName"]);
        }
    }
}
