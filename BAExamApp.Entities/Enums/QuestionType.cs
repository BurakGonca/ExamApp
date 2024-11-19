using System.ComponentModel.DataAnnotations;

namespace BAExamApp.Entities.Enums;

public enum QuestionType
{
    [Display(Name = "Multiple_Choice")]
    MultipleAnswer = 1,
    [Display(Name = "Test")]
    Test = 2,
    [Display(Name = "True_False")]
    TrueFalse = 3,
    //[Display(Name = "Text_Answered_Question")]
    //Classic = 4,
    //[Display(Name = "Fill_Blank")]
    //FillBlank = 5,
    //[Display(Name = "Documentation")]
    //Documentation = 6,
    //[Display(Name = "Compile")]
    //Compile = 7,
}
