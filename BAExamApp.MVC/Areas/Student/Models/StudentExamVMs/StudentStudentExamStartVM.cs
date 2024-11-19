using System.ComponentModel.DataAnnotations;
using System.Security.Policy;

namespace BAExamApp.MVC.Areas.Student.Models.StudentExamVMs;

public class StudentStudentExamStartVM
{
    [Display(Name = "Id")]
    public string StudentExamId { get; set; }

    public string StudentFullName { get; set; }
    [Display(Name = "Exam_Name")]
    public string ExamName { get; set; }
    [Display(Name = "Exam_Date_Time")]
    public DateTime ExamDateTime { get; set; }
    [Display(Name = "Exam_Duration")]
    public TimeSpan ExamDuration { get; set; }
    [Display(Name = "Question_Count")]
    public int QuestionCount { get; set; }

    [Display(Name = "Excuse")]
    public string? ExcuseDescription { get; set; }

    public DateTime? RetakeExamDate { get; set; }


}
