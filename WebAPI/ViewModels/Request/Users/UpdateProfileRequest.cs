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
        /// Address
        /// </summary>
        /// <example>27 Park St Centereach</example>
        public string Address { get; set; }
        /// <summary>
        /// Postal code
        /// </summary>
        /// <example>33014</example>
        public string PostalCode { get; set; }
        /// <summary>
        /// Country identifier
        /// </summary>
        /// <example>2</example>
        public int? CountryId { get; set; }
        /// <summary>
        /// City identifier
        /// </summary>
        /// <example>1</example>
        public int? CityId { get; set; }
        /// <summary>
        /// Gender identifier
        /// </summary>
        /// <example>3</example>
        public int? GenderId { get; set; }
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
