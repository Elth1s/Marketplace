using FluentValidation;
using Microsoft.Extensions.Localization;

namespace WebAPI.ViewModels.Request.Reviews
{
    /// <summary>
    /// Review reply class to create review reply
    /// </summary>
    public class ReviewReplyRequest
    {
        /// <summary>
        /// User full name
        /// </summary>
        /// <example>Nick Smith</example>
        public string FullName { get; set; }
        /// <summary>
        /// User email address
        /// </summary>
        /// <example>email@gmail.com</example>
        public string Email { get; set; }
        /// <summary>
        /// Text
        /// </summary>
        /// <example>Some information</example>
        public string Text { get; set; }
        /// <summary>
        /// Review id
        /// </summary>
        /// <example>1</example>
        public int ReviewId { get; set; }
    }

    /// <summary>
    /// Class for <seealso cref="ReviewReplyRequest" /> validation
    /// </summary>
    public class ReviewReplyRequestValidator : AbstractValidator<ReviewReplyRequest>
    {
        private readonly IStringLocalizer<ValidationResourсes> _validationResources;
        public ReviewReplyRequestValidator(IStringLocalizer<ValidationResourсes> validationResources)
        {
            _validationResources = validationResources;

            //Full name
            RuleFor(x => x.FullName).Cascade(CascadeMode.Stop)
               .NotEmpty().WithName(_validationResources["FullNamePropName"]).WithMessage(_validationResources["PluralRequiredMessage"])
               .Length(2, 80).WithMessage(_validationResources["PluralLengthMessage"]);

            //Email
            RuleFor(x => x.Email).Cascade(CascadeMode.Stop)
              .NotEmpty().WithName(_validationResources["EmailAddressPropName"]).WithMessage(_validationResources["RequiredMessage"])
              .EmailAddress();

            //Text
            RuleFor(a => a.Text).Cascade(CascadeMode.Stop)
               .NotEmpty().WithName(_validationResources["TextPropName"])
               .Length(1, 600);
        }
    }
}
