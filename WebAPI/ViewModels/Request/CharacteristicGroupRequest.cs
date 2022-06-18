using DAL;
using DAL.Entities;
using FluentValidation;

namespace WebAPI.ViewModels.Request
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
               .Length(2, 30).WithMessage("{PropertyName} should be between 2 and 30 characters");
        }
    }
}
