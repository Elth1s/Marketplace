using DAL;
using DAL.Entities;
using FluentValidation;
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
        private readonly IRepository<Unit> _unitRepository;
        public UnitRequestValidator(IRepository<Unit> unitRepository)
        {
            _unitRepository = unitRepository;

            //Measure
            RuleFor(x => x.Measure).Cascade(CascadeMode.Stop)
               .NotEmpty().WithName("Measure").WithMessage("{PropertyName} is required")
               .Must(IsUniqueName).WithMessage("Unit with this {PropertyName} already exists")
               .Length(1, 30).WithMessage("{PropertyName} should be between 1 and 30 characters");
        }

        private bool IsUniqueName(string measure)
        {
            var spec = new UnitGetByMeasureSpecification(measure);
            return _unitRepository.GetBySpecAsync(spec).Result == null;
        }
    }
}
