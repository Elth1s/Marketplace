using FluentValidation;

namespace WebAPI.ViewModels.Request
{
    /// <summary>
    /// Country class to create and update country 
    /// </summary>
    public class CountryRequest
    {
        /// <summary>
        /// Name of country
        /// </summary>
        /// <example>USA</example>
        public string Name { get; set; }
    }

    /// <summary>
    /// Class for <seealso cref="CountryRequest" /> validation
    /// </summary>
    public class CountryRequestValidator : AbstractValidator<CountryRequest>
    {
        public CountryRequestValidator()
        {
            //Name
            RuleFor(x => x.Name).Cascade(CascadeMode.Stop)
               .NotEmpty().WithName("Name").WithMessage("{PropertyName} is required")
               .Length(2, 30).WithMessage("{PropertyName} should be between 2 and 30 characters");
        }
    }
}
