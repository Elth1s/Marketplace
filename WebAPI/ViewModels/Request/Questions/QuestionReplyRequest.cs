﻿using FluentValidation;

namespace WebAPI.ViewModels.Request.Questions
{
    /// <summary>
    /// Question reply class to create question reply
    /// </summary>
    public class QuestionReplyRequest
    {
        /// <summary>
        /// User full name
        /// </summary>
        /// <example>Nick Smith</example>
        public string FullName { get; set; }
        /// <summary>
        /// User email address
        /// </summary>
        /// <example>email@gmail.com</example>
        public string Email { get; set; }
        /// <summary>
        /// Text
        /// </summary>
        /// <example>Some information</example>
        public string Text { get; set; }
        /// <summary>
        /// Question id
        /// </summary>
        /// <example>1</example>
        public int QuestionId { get; set; }
    }

    /// <summary>
    /// Class for <seealso cref="QuestionReplyRequest" /> validation
    /// </summary>
    public class QuestionReplyRequestValidator : AbstractValidator<QuestionReplyRequest>
    {
        public QuestionReplyRequestValidator()
        {
            //Full name
            RuleFor(x => x.FullName).Cascade(CascadeMode.Stop)
               .NotEmpty().WithName("Full name").WithMessage("{PropertyName} is required")
               .Length(2, 80).WithMessage("{PropertyName} should be between 2 and 60 characters");

            //Email
            RuleFor(x => x.Email).Cascade(CascadeMode.Stop)
              .NotEmpty().WithName("Email address").WithMessage("{PropertyName} is required")
              .EmailAddress().WithMessage("Invalid format of {PropertyName}"); ;

            //Text
            RuleFor(a => a.Text).Cascade(CascadeMode.Stop)
               .NotEmpty().WithName("Text").WithMessage("{PropertyName} is required!")
               .Length(1, 600).WithMessage("{PropertyName} should be between 1 and 600 characters");
        }
    }
}
