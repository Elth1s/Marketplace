﻿using DAL.Entities.Identity;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using WebAPI.ViewModels.Request;

namespace WebAPI.Validators
{
    public class SignUpRequestValidator : AbstractValidator<SignUpRequest>
    {
        private readonly UserManager<AppUser> _userManager;
        public SignUpRequestValidator(UserManager<AppUser> userManager)
        {
            _userManager = userManager;

            //First name
            RuleFor(x => x.FirstName).Cascade(CascadeMode.Stop)
               .NotEmpty().WithName("First name").WithMessage("{PropertyName} is required")
               .Length(2, 15).WithMessage("{PropertyName} should be between 2 and 15 characters");

            //Second name
            RuleFor(x => x.SecondName).Cascade(CascadeMode.Stop)
              .NotEmpty().WithName("Second name").WithMessage("{PropertyName} is required")
              .Length(2, 40).WithMessage("{PropertyName} should be between 2 and 40 characters");

            //Phone
            RuleFor(x => x.Phone).Cascade(CascadeMode.Stop)
              .NotEmpty().WithName("Phone number").WithMessage("{PropertyName} is required")
              .Matches(@"^((\\+[1-9]{1,4}[ \\-]*)|(\\([0-9]{2,3}\\)[ \\-]*)|([0-9]{2,4})[ \\-]*)*?[0-9]{3,4}?[ \\-]*[0-9]{3,4}?$")
              .WithMessage("Invalid format of {PropertyName}")
              .Must(IsUniquePhone).WithMessage("User with this {PropertyName} already exists");

            //Email
            RuleFor(x => x.Email).Cascade(CascadeMode.Stop)
               .NotEmpty().WithName("Email address").WithMessage("{PropertyName} is required")
               .EmailAddress().WithMessage("Invalid format of {PropertyName}")
               .Must(IsUniqueEmail).WithMessage("User with this {PropertyName} already exists");

            //Password
            RuleFor(x => x.Password).Cascade(CascadeMode.Stop)
           .NotEmpty().WithMessage("{PropertyName} is required")
           .MinimumLength(8).WithMessage("{PropertyName} must be at least 8 characters")
           .Matches(@"(?=.*[A-Z])").WithMessage("{PropertyName} must contain at least one lowercase letter")
           .Matches(@"(?=.*[A-Z])").WithMessage("{PropertyName} must contain at least one uppercase letter")
           .Matches(@"(?=.*?[0-9])").WithMessage("{PropertyName} must contain at least one digit")
           .Matches(@"(?=.*?[!@#\$&*~_-])").WithMessage("{PropertyName} must contain at least one special character");

            //ConfirmPassword
            RuleFor(x => x.ConfirmPassword).Cascade(CascadeMode.Stop)
               .NotEmpty().WithName("Confirm Password").WithMessage("{PropertyName} is required")
               .Equal(x => x.Password).WithMessage("Password and {PropertyName} do not match");


        }
        private bool IsUniqueEmail(string email)
        {
            return _userManager.FindByEmailAsync(email).Result == null;
        }
        private bool IsUniquePhone(string phone)
        {
            return _userManager.Users.FirstOrDefault(u => u.PhoneNumber == phone) == null;
        }
    }
}
