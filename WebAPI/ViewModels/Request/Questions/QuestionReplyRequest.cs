using FluentValidation;
using Microsoft.Extensions.Localization;

namespace WebAPI.ViewModels.Request.Questions
{
    /// <summary>
    /// Question reply class to create question reply
    /// </summary>
    public class QuestionReplyRequest
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
        /// Question id
        /// </summary>
        /// <example>1</example>
        public int QuestionId { get; set; }
    }

    /// <summary>
    /// Class for <seealso cref="QuestionReplyRequest" /> validation
    /// </summary>
    public class QuestionReplyRequestValidator : AbstractValidator<QuestionReplyRequest>
    {
        private readonly IStringLocalizer<ValidationResourсes> _validationResources;
        public QuestionReplyRequestValidator(IStringLocalizer<ValidationResourсes> validationResources)
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
