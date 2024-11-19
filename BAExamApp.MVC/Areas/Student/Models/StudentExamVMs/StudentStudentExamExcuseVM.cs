using System.ComponentModel.DataAnnotations;

namespace BAExamApp.MVC.Areas.Student.Models.StudentExamVMs;

public class StudentStudentExamExcuseVM
{
    [Display(Name = "Exam_Name")]
    [Required]
    public string ExamName { get; set; }
    [Display(Name = "Exam_Date_Time")]
    [Required]
    public DateTime ExamDateTime { get; set; }
    [Display(Name = "Exam_Duration")]
    [Required]
    public TimeSpan ExamDuration { get; set; }
    [Display(Name = "Excuse")]
    public string? ExcuseDescription { get; set; }
}
