using DAL;
using DAL.Entities;
using FluentValidation;
using WebAPI.Specifications.Characteristics;

namespace WebAPI.ViewModels.Request.Characteristics
{
    /// <summary>
    /// Characteristic group class to create and update characteristic group
    /// </summary>
    public class CharacteristicGroupRequest
    {
        /// <summary>
        /// Name of characteristic
        /// </summary>
        /// <example>Main</example>
        public string Name { get; set; }
    }

    /// <summary>
    /// Class for <seealso cref="CharacteristicGroupRequest" /> validation
    /// </summary>
    public class CharacteristicGroupRequestValidation : AbstractValidator<CharacteristicGroupRequest>
    {
        private readonly IRepository<CharacteristicGroup> _characteristicGroupRequest;

        public CharacteristicGroupRequestValidation(IRepository<CharacteristicGroup> characteristicGroupRequest)
        {
            _characteristicGroupRequest = characteristicGroupRequest;

            //Name
            RuleFor(x => x.Name).Cascade(CascadeMode.Stop)
               .NotEmpty().WithName("Name").WithMessage("{PropertyName} is required")
               .Must(IsUniqueName).WithMessage("Characteristic group with this {PropertyName} already exists")
               .Length(2, 30).WithMessage("{PropertyName} should be between 2 and 30 characters");
        }

        private bool IsUniqueName(string name)
        {
            var spec = new CharacteristicGroupGetByNameSpecification(name);
            return _characteristicGroupRequest.GetBySpecAsync(spec).Result == null;
        }
    }
}
