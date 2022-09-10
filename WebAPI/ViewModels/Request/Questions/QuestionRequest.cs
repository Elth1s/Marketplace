using FluentValidation;
using Microsoft.Extensions.Localization;

namespace WebAPI.ViewModels.Request.Questions
{
    /// <summary>
    /// Question class to create question 
    /// </summary>
    public class QuestionRequest
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
        /// Message 
        /// </summary>
        /// <example>Some information</example>

        public string Message { get; set; }

        /// <summary>
        /// Product slug
        /// </summary>
        /// <example>some-product-slug</example>
        public string ProductSlug { get; set; }

        /// <summary>
        /// List of images
        /// </summary>
        public ICollection<string> Images { get; set; }
    }


    /// <summary>
    /// Class for <seealso cref="QuestionRequest" /> validation
    /// </summary>
    public class QuestionRequestValidator : AbstractValidator<QuestionRequest>
    {
        private readonly IStringLocalizer<ValidationResourсes> _validationResources;
        public QuestionRequestValidator(IStringLocalizer<ValidationResourсes> validationResources)
        {
            _validationResources = validationResources;

            //Full Name
            RuleFor(x => x.FullName).Cascade(CascadeMode.Stop)
               .NotEmpty().WithName(_validationResources["FullNamePropName"]).WithMessage(_validationResources["PluralRequiredMessage"])
               .Length(2, 80).WithMessage(_validationResources["PluralLengthMessage"]);

            //Message
            RuleFor(x => x.Message).Cascade(CascadeMode.Stop)
               .NotEmpty().WithName(_validationResources["MessagePropName"])
               .Length(2, 500);

            //Email
            RuleFor(x => x.Email).Cascade(CascadeMode.Stop)
               .NotEmpty().WithName(_validationResources["EmailAddressPropName"]).WithMessage(_validationResources["RequiredMessage"])
                .EmailAddress();

            //Product slug
            RuleFor(a => a.ProductSlug).Cascade(CascadeMode.Stop)
                 .NotEmpty().WithName(_validationResources["ProductUrlSlugPropName"])
                 .WithMessage(_validationResources["RequiredMessage"]);
        }
    }

}
