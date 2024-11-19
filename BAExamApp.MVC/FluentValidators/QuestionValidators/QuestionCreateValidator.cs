using BAExamApp.MVC.Areas.Trainer.Models.QuestionVMs;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace BAExamApp.MVC.FluentValidators.QuestionValidators;

public class QuestionCreateValidator : AbstractValidator<TrainerQuestionCreateVM>
{
    public QuestionCreateValidator(IStringLocalizer<TrainerQuestionCreateVM> stringLocalizer)
    {
        RuleFor(t0 => t0.ProductId).Must((rootObject, list) => rootObject.ProductId != Guid.Empty).WithMessage(stringLocalizer["Please_Choose_Product"]);
        RuleFor(t0 => t0.SubjectId).Must((rootObject, list) => rootObject.SubjectId != Guid.Empty).WithMessage(stringLocalizer["Please_Choose_Subject"]);
        RuleFor(t0 => t0.SubtopicId).Must(subtopicList => subtopicList != null && subtopicList.Any(subtopic => subtopic != Guid.Empty)).WithMessage(stringLocalizer["Please_Choose_Subtopic"]);
        RuleFor(t0 => t0.QuestionType).Must((rootObject, list) => rootObject.QuestionType > 0).WithMessage(stringLocalizer["Please_Choose_QuestionType"]);
        RuleFor(t0 => t0.QuestionDifficultyId).Must((rootObject, list) => rootObject.QuestionDifficultyId != Guid.Empty).WithMessage(stringLocalizer["Please_Choose_QuestionDifficulty"]);
        RuleFor(t0 => t0.Target).NotEmpty().WithMessage(stringLocalizer["Please_Do_Not_Leave_QuestionTarget_Blank"]);
        RuleFor(t0 => t0.Gains).NotEmpty().WithMessage(stringLocalizer["Please_Do_Not_Leave_QuestionGains_Blank"]);
        RuleFor(t0 => t0.Content).NotEmpty().WithMessage(stringLocalizer["Please_Do_Not_Leave_QuestionContent_Blank"]);
    }
}