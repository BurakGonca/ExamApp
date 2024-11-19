using System.ComponentModel.DataAnnotations;

namespace BAExamApp.MVC.Areas.CandidateAdmin.Models.CandidateExamInitiationVMs;

public class CandidateExamStartVM
{
    public Guid ExamId { get; set; }
    public Guid CandidateId { get; set; }

    [Display(Name = "Full_Name")]
    public string FullName { get; set; } = null!;

    [Display(Name = "Exam_Name")]
    public string ExamName { get; set; } = null!;

    [Display(Name = "Exam_Date_Time")]
    public DateTime ExamDateTime { get; set; }

    [Display(Name = "Exam_Duration")]
    public TimeSpan ExamDuration { get; set; }

    [Display(Name = "Question_Count")]
    public int QuestionCount { get; set; }

    [Display(Name = "Excuse")]
    public string? ExcuseDescription { get; set; }
}
