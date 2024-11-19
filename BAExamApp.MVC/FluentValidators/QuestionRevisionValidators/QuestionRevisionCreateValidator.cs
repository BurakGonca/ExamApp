using BAExamApp.Entities.Enums;
using BAExamApp.MVC.Areas.Admin.Models.QuestionVMs;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace BAExamApp.MVC.FluentValidators.QuestionReviewValidators;

public class QuestionRevisionCreateValidator : AbstractValidator<AdminQuestionReviewVM>
{
    public QuestionRevisionCreateValidator(IStringLocalizer<AdminQuestionReviewVM> stringLocalizer)
    {
        RuleFor(t0 => t0.TrainerID).Must((rootObject, list) => rootObject.TrainerID != Guid.Empty).WithMessage(stringLocalizer["Please_Choose_Trainer"]); 
        RuleFor(t0 => t0.RequestDescription).NotEmpty().WithMessage(stringLocalizer["Please_Do_Not_Leave_RequestDescription_Blank"]);
        RuleFor(t0 => t0.RejectComment).NotEmpty().NotNull().WithMessage(stringLocalizer["Please_Do_Not_Leave_Reject_Text_Blank"]).When(t0 => t0.State == State.Rejected);
    }
}