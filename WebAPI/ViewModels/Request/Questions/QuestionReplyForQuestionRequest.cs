using FluentValidation;
using Microsoft.Extensions.Localization;

namespace WebAPI.ViewModels.Request.Questions
{
    /// <summary>
    /// Question reply for review class to pagination question reply
    /// </summary>
    public class QuestionReplyForQuestionRequest
    {
        /// <summary>
        /// Question identifier
        /// </summary>
        /// <example>1</example>
        public int QuestionId { get; set; }
        /// <summary>
        /// Page
        /// </summary>
        /// <example>1</example>
        public int Page { get; set; }

        /// <summary>
        /// Row per page
        /// </summary>
        /// <example>8</example>
        public int RowsPerPage { get; set; }
    }

    /// <summary>
    /// Class for <seealso cref="QuestionReplyForQuestionRequest" /> validation
    /// </summary>
    public class QuestionReplyForQuestionRequestValidation : AbstractValidator<QuestionReplyForQuestionRequest>
    {
        private readonly IStringLocalizer<ValidationResourсes> _validationResources;
        public QuestionReplyForQuestionRequestValidation(IStringLocalizer<ValidationResourсes> validationResources)
        {
            _validationResources = validationResources;

            //Page
            RuleFor(a => a.Page).Cascade(CascadeMode.Stop)
                .NotEmpty().WithName(_validationResources["PagePropName"])
                .WithMessage(_validationResources["RequiredMessage"])
                .GreaterThan(0).WithMessage(_validationResources["GreaterThanMessage"]);

            //RowsPerPage
            RuleFor(a => a.RowsPerPage).Cascade(CascadeMode.Stop)
                .NotEmpty().WithName(_validationResources["RowsPerPagePropName"])
                .WithMessage(_validationResources["PluralRequiredMessage"])
                .GreaterThan(0).WithMessage(_validationResources["PluralGreaterThanMessage"]);
        }
    }
}
