using BAExamApp.MVC.Areas.Admin.Models.ExamRuleVMs;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace BAExamApp.MVC.FluentValidators.ExamRuleValidators;

public class ExamRuleUpdateValidator : AbstractValidator<AdminExamRuleUpdateVM>
{
    public ExamRuleUpdateValidator(IStringLocalizer<AdminExamRuleUpdateVM> stringLocalizer)
    {
        RuleFor(x => x.Name).NotNull().WithMessage(stringLocalizer["Please_Do_Not_Leave_Exam_Rule_Name_Blank"])
                            .NotEmpty().WithMessage(stringLocalizer["Please_Do_Not_Leave_Exam_Rule_Name_Blank"])
                            .MinimumLength(2).WithMessage(stringLocalizer["Exam_Rule_Name_Minimum_2_Character"])
                            .MaximumLength(256).WithMessage(stringLocalizer["Exam_Rule_Name_Maximum_256_Character"]);
        RuleFor(x => x.ProductId).NotNull().WithMessage(stringLocalizer["Please_Select_Product"])
                                   .NotEmpty().WithMessage(stringLocalizer["Please_Select_Product"]);
        RuleFor(x => x.ExamRuleSubtopics).NotNull().WithMessage(stringLocalizer["Please_Add_At_Least_One_Exam_Rule"])
                                        .ForEach(x => x.Must(y => y.QuestionCount > 0).WithMessage(stringLocalizer["Question_Count_Must_Positive"]));
    }
}