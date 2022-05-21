﻿using FluentValidation;
using WebAPI.ViewModels.Request;

namespace WebAPI.Validators
{
    public class CharacteristicRequestValidator : AbstractValidator<CharacteristicRequest>
    {
        public CharacteristicRequestValidator()
        {
            //Name
            RuleFor(x => x.Name).Cascade(CascadeMode.Stop)
               .NotEmpty().WithName("Name").WithMessage("{PropertyName} is required")
               .Length(2, 30).WithMessage("{PropertyName} should be between 2 and 30 characters");
        }
    }
}