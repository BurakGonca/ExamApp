using System.ComponentModel.DataAnnotations;

namespace BAExamApp.MVC.Areas.Admin.Models.ExamRuleSubtopicVMs;

public class AdminExamRuleSubtopicCreateVM
{
    [Display(Name = "Question_Subject")]
    public Guid SubjectId { get; set; }

    [Display(Name = "Question_SubTopic")]
    public Guid SubtopicId { get; set; }

    [Display(Name = "Exam_Type")]
    public int ExamType { get; set; }

    [Display(Name = "Question_Type")]
    public int QuestionType { get; set; }

    [Display(Name = "Question_Difficulty")]
    public Guid QuestionDifficultyId { get; set; }

    [Display(Name = "Question_Count")]
    public int QuestionCount { get; set; }
}