using BAExamApp.Entities.Enums;
using System.ComponentModel.DataAnnotations;

namespace BAExamApp.MVC.Areas.Admin.Models.QuestionVMs;

public class AdminQuestionAprovedListVM
{
    public Guid Id { get; set; }

    [Display(Name = "Question_Content")]
    public string Content { get; set; }

    [Display(Name = "Question_Difficulty")]
    public string QuestionDifficultyName { get; set; }

    [Display(Name = "Question_ModifiedDate")]
    public DateTime ModifiedDate { get; set; }

    [Display(Name = "Question_SubjectName")]
    public string SubjectName { get; set; }

    [Display(Name = "Modified_By")]
    public string ModifiedBy { get; set; }

    [Display(Name = "Question_ModifierFullName")]
    public string ModifierName { get; set; }

    [Display(Name = "Question_IsActive")]
    public bool IsActive { get; set; }

}