using FluentValidation;

namespace WebAPI.ViewModels.Request.Users
{
    /// <summary>
    /// Class for external user login 
    /// </summary>
    public class ExternalLoginRequest
    {
        /// <summary>
        /// Provider for external login
        /// </summary>
        /// <example>Google</example>
        public string Provider { get; set; }
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
        public ExternalLoginRequestValidator()
        {
            RuleFor(x => x.Provider).Cascade(CascadeMode.Stop)
                .NotNull()
                .NotEmpty();

            RuleFor(x => x.Token).Cascade(CascadeMode.Stop)
                .NotNull()
                .NotEmpty();
        }
    }
}
