using System.ComponentModel.DataAnnotations;

namespace BAExamApp.Entities.Enums;
public enum CandidateQuestionType
{
    [Display(Name = "Test")]
    Test = 1,
    [Display(Name = "Algorithm")]
    Algorithm = 2,
    [Display(Name = "Text_Answered_Question")]
    Classic = 3,
}
