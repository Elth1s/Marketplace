using FluentValidation;

namespace WebAPI.ViewModels.Request
{
    /// <summary>
    /// City class to create and update city
    /// </summary>
    public class CityRequest
    {
        /// <summary>
        /// City name
        /// </summary>
        /// <example>Atlanta</example>
        public string Name { get; set; }
        /// <summary>
        /// Country identifier
        /// </summary>
        /// <example>1</example>
        public int CountryId { get; set; }
    }

    /// <summary>
    /// Class for <seealso cref="CityRequest" /> validation
    /// </summary>
    public class CityRequestValidator : AbstractValidator<CityRequest>
    {
        public CityRequestValidator()
        {
            //Name
            RuleFor(x => x.Name).Cascade(CascadeMode.Stop)
               .NotEmpty().WithName("Name").WithMessage("{PropertyName} is required")
               .Length(2, 30).WithMessage("{PropertyName} should be between 2 and 30 characters");
        }
    }
}
