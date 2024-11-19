using BAExamApp.Entities.Enums;
using System.ComponentModel.DataAnnotations;

namespace BAExamApp.MVC.Areas.Admin.Models.ExamVMs;

public class StudentExamDetailForAdminVM
{
    public Guid Id { get; set; }
    public Guid StudentId { get; set; }

    [Display(Name = "Student_Name")]
    public string StudentName { get; set; }

    [Display(Name = "Exam_Name")]
    public string ExamName { get; set; }

    [Display(Name = "Exam_Score")]
    public decimal? Score { get; set; }

    [Display(Name = "Max_Score")]
    public decimal MaxScore { get; set; }

    [Display(Name = "Classroom")]
    public List<string> ClassroomNames { get; set; }

    [Display(Name = "Trainer")]
    public string? EvaluatorName { get; set; }

    [Display(Name = "Exam_Type")]
    public ExamType ExamType { get; set; }

    [Display(Name = "Excuse")]
    public string? ExcuseDescription { get; set; }

}
