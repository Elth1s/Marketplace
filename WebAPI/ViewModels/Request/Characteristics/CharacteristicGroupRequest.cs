using DAL;
using DAL.Entities;
using FluentValidation;
using Microsoft.Extensions.Localization;
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
        private readonly IStringLocalizer<ValidationResourсes> _validationResources;

        private readonly IRepository<CharacteristicGroup> _characteristicGroupRequest;
        public CharacteristicGroupRequestValidation(IRepository<CharacteristicGroup> characteristicGroupRequest,
            IStringLocalizer<ValidationResourсes> validationResources)
        {
            _validationResources = validationResources;

            _characteristicGroupRequest = characteristicGroupRequest;

            //Name
            RuleFor(x => x.Name).Cascade(CascadeMode.Stop)
               .NotEmpty().WithName(_validationResources["NamePropName"])
               .Must(IsUniqueName).WithMessage("Characteristic group with this {PropertyName} already exists")
               .Length(2, 30);
        }

        private bool IsUniqueName(string name)
        {
            var spec = new CharacteristicGroupGetByNameSpecification(name);
            return _characteristicGroupRequest.GetBySpecAsync(spec).Result == null;
        }
    }
}
