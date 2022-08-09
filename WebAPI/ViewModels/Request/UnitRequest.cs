using DAL;
using DAL.Entities;
using FluentValidation;
using Microsoft.Extensions.Localization;
using WebAPI.Specifications.Units;

namespace WebAPI.ViewModels.Request
{
    /// <summary>
    /// Unit class to create and update unit 
    /// </summary>
    public class UnitRequest
    {
        /// <summary>
        /// Unit measure
        /// </summary>
        /// <example>m</example>
        public string Measure { get; set; }
    }

    /// <summary>
    /// Class for <seealso cref="UnitRequest" /> validation
    /// </summary>
    public class UnitRequestValidator : AbstractValidator<UnitRequest>
    {
        private readonly IStringLocalizer<ValidationResourсes> _validationResources;
        private readonly IRepository<Unit> _unitRepository;
        public UnitRequestValidator(IRepository<Unit> unitRepository,
            IStringLocalizer<ValidationResourсes> validationResources)
        {
            _unitRepository = unitRepository;
            _validationResources = validationResources;

            //Measure
            RuleFor(x => x.Measure).Cascade(CascadeMode.Stop)
               .NotEmpty().WithName(_validationResources["MeasurePropName"]).WithMessage(_validationResources["RequiredMessage"])
               .Length(1, 30)
               .Must(IsUniqueMeasure).WithMessage(_validationResources["UnitUniqueMeasureMessage"]);
        }

        private bool IsUniqueMeasure(string measure)
        {
            var spec = new UnitGetByMeasureSpecification(measure);
            return _unitRepository.GetBySpecAsync(spec).Result == null;
        }
    }
}
