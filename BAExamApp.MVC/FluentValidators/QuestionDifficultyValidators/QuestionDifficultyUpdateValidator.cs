using BAExamApp.MVC.Areas.Admin.Models.QuestionDifficultyVMs;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace BAExamApp.MVC.FluentValidators.QuestionDifficultyValidators;

public class QuestionDifficultyUpdateValidator : AbstractValidator<AdminQuestionDifficultyUpdateVM>
{
    public QuestionDifficultyUpdateValidator(IStringLocalizer<AdminQuestionDifficultyUpdateVM> stringLocalizer)
    {
        RuleFor(x => x.Name)
            .NotNull().WithMessage(stringLocalizer["Please_Do_Not_Leave_Question_Difficulty_Name_Blank"])
            .NotEmpty().WithMessage(stringLocalizer["Please_Do_Not_Leave_Question_Difficulty_Name_Blank"])
            .MinimumLength(3).WithMessage(stringLocalizer["Question_Difficulty_Name_Minimum_3_Character"])
            .MaximumLength(10).WithMessage(stringLocalizer["Question_Difficulty_Name_Maximum_10_Character"]);
        RuleFor(x => x.TimeGiven)
            .NotNull().WithMessage(stringLocalizer["Please_Do_Not_Leave_Time_Given_Blank"])
            .NotEmpty().WithMessage(stringLocalizer["Please_Do_Not_Leave_Time_Given_Blank"]);
        RuleFor(x => x.ScoreCoefficient)
            .NotNull().WithMessage(stringLocalizer["Please_Do_Not_Leave_Score_Coefficient_Blank"])
            .NotEmpty().WithMessage(stringLocalizer["Please_Do_Not_Leave_Score_Coefficient_Blank"]);

    }
}
