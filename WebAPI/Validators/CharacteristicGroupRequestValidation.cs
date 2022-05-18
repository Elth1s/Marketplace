using DAL;
using DAL.Entities;
using FluentValidation;
using WebAPI.ViewModels.Request;

namespace WebAPI.Validators
{
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
