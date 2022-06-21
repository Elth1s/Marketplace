using FluentValidation;

namespace WebAPI.ViewModels.Request.Users
{
    /// <summary>
    /// Profile class to update user profile
    /// </summary>
    public class UpdateProfileRequest
    {
        /// <summary>
        /// User first name
        /// </summary>
        /// <example>Nick</example>
        public string FirstName { get; set; }
        /// <summary>
        /// User second name
        /// </summary>
        /// <example>Smith</example>
        public string SecondName { get; set; }
        /// <summary>
        /// User name
        /// </summary>
        /// <example>someUserName</example>
        public string UserName { get; set; }
        /// <summary>
        /// User photo
        /// </summary>
        public string Photo { get; set; }
    }

    /// <summary>
    /// Class for <seealso cref="UpdateProfileRequest" /> validation
    /// </summary>
    public class UpdateProfileRequestValidator : AbstractValidator<UpdateProfileRequest>
    {
        public UpdateProfileRequestValidator()
        {
            //First name
            RuleFor(x => x.FirstName).Cascade(CascadeMode.Stop)
               .NotEmpty().WithName("First name").WithMessage("{PropertyName} is required")
               .Length(2, 15).WithMessage("{PropertyName} should be between 2 and 15 characters");

            //Second name
            RuleFor(x => x.SecondName).Cascade(CascadeMode.Stop)
              .NotEmpty().WithName("Second name").WithMessage("{PropertyName} is required")
              .Length(2, 40).WithMessage("{PropertyName} should be between 2 and 40 characters");

            //User name
            RuleFor(x => x.UserName).Cascade(CascadeMode.Stop)
              .NotEmpty().WithName("User name").WithMessage("{PropertyName} is required")
              .Length(2, 40).WithMessage("{PropertyName} should be between 2 and 40 characters");
        }
    }
}
