using BAExamApp.Entities.Enums;
using System.ComponentModel.DataAnnotations;

namespace BAExamApp.MVC.Areas.Admin.Models.ExamRuleSubtopicVMs;

public class AdminExamRuleSubtopicDetailVM
{
    [Display(Name = "Question_SubjectName")]
    public string SubjectName { get; set; }

    [Display(Name = "Question_SubtopicName")]
    public string SubtopicName { get; set; }

    [Display(Name = "Exam_Type")]
    public ExamType ExamType { get; set; }

    [Display(Name = "Question_Type")]
    public QuestionType QuestionType { get; set; }

    [Display(Name = "Question_Difficulty")]
    public string QuestionDifficultyName { get; set; }

    [Display(Name = "Version")]
    public string VersionTypeName { get; set; }

    [Display(Name = "Question_Count")]
    public int QuestionCount { get; set; }
}
