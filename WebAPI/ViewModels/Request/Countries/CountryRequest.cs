using FluentValidation;

namespace WebAPI.ViewModels.Request.Countries
{
    /// <summary>
    /// Country class to create and update country 
    /// </summary>
    public class CountryRequest
    {
        /// <summary>
        /// Name of country
        /// </summary>
        /// <example>Ukraine</example>
        public string Name { get; set; }
        /// <summary>
        /// Code of country
        /// </summary>
        /// <example>UA</example>
        public string Code { get; set; }
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
               .Length(2, 60).WithMessage("{PropertyName} should be between 2 and 60 characters");

            //Code
            RuleFor(c => c.Code).Cascade(CascadeMode.Stop)
                   .NotEmpty().WithMessage("{PropertyName} is required!")
                   .Length(2).WithMessage("{PropertyName} must be 2 characters long.");
        }

    }
}
