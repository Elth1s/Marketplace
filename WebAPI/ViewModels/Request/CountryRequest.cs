using DAL;
using DAL.Entities;
using FluentValidation;
using WebAPI.Specifications;

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
        private readonly IRepository<Country> _countryRepository;
        public CountryRequestValidator(IRepository<Country> countryRepository)
        {
            _countryRepository = countryRepository;

            //Name
            RuleFor(x => x.Name).Cascade(CascadeMode.Stop)
               .NotEmpty().WithName("Name").WithMessage("{PropertyName} is required")
               .Must(IsUniqueName).WithMessage("Country with this {PropertyName} already exists")
               .Length(2, 30).WithMessage("{PropertyName} should be between 2 and 30 characters");
        }

        private bool IsUniqueName(string name)
        {
            var spec = new CountryGetByNameSpecification(name);
            return _countryRepository.GetBySpecAsync(spec).Result == null;
        }
    }
}
