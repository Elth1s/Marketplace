using FluentValidation;
using Microsoft.Extensions.Localization;

namespace WebAPI.ViewModels.Request.Users
{
    /// <summary>
    /// Profile class to update user profile
    /// </summary>
    public class UpdateProfileRequest
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
        /// User photo
        /// </summary>
        public string Photo { get; set; }
    }

    /// <summary>
    /// Class for <seealso cref="UpdateProfileRequest" /> validation
    /// </summary>
    public class UpdateProfileRequestValidator : AbstractValidator<UpdateProfileRequest>
    {
        private readonly IStringLocalizer<ValidationResourсes> _validationResources;
        public UpdateProfileRequestValidator(IStringLocalizer<ValidationResourсes> validationResources)
        {
            _validationResources = validationResources;

            //First name
            RuleFor(x => x.FirstName).Cascade(CascadeMode.Stop)
               .NotEmpty().WithName(_validationResources["FirstNamePropName"])
               .Length(2, 15);

            //Second name
            RuleFor(x => x.SecondName).Cascade(CascadeMode.Stop)
              .NotEmpty().WithName(_validationResources["SecondNamePropName"])
              .Length(2, 40);
        }
    }
}
