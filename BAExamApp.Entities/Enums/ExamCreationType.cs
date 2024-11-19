using System.ComponentModel.DataAnnotations;

namespace BAExamApp.Entities.Enums;
public enum ExamCreationType
{
    [Display(Name = "Same_For_Everyone")]
    SameForEveryone = 1,
    [Display(Name = "Same_Questions_Different_Order")]
    SameQuestionsDifferentOrder = 2,
    [Display(Name = "Different_Questions")]
    DifferentQuestions = 3,
}
