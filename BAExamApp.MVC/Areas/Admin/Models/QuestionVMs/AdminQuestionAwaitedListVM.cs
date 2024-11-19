using BAExamApp.Entities.Enums;
using System.ComponentModel.DataAnnotations;

namespace BAExamApp.MVC.Areas.Admin.Models.QuestionVMs;

public class AdminQuestionAwaitedListVM
{
    public Guid Id { get; set; }

    [Display(Name = "Question_Content")]
    public string Content { get; set; }

    [Display(Name = "Question_Difficulty")]
    public string QuestionDifficultyName { get; set; }

    [Display(Name = "Question_CreatedDate")]
    public DateTime CreatedDate { get; set; }

    [Display(Name = "Subtopic")]
    public string SubtopicName { get; set; }

    [Display(Name = "Question_IsActive")]
    public bool IsActive { get; set; }
    [Display(Name = "Question_Time")]
    public TimeSpan Time { get; set; }
}