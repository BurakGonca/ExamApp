using BAExamApp.Core.Enums;
using System.ComponentModel.DataAnnotations;

namespace BAExamApp.MVC.Areas.Trainer.Models.StudentExamVMs;

public class StudentExamForTrainerVM
{
    public Guid Id { get; set; }

    [Display(Name = "Exam_Score")]
    public decimal? Score { get; set; }

    [Display(Name = "Exam_Name")]
    public string ExamName { get; set; }

    [Display(Name = "Max_Score")]
    public string MaxScore { get; set; }
    public DateTime ExamDateTime { get; set; }

    [Display(Name = "Classrooms")]
    public List<string>? ClassroomNames { get; set; }

    [Display(Name = "Student_Full_Name")]
    public string StudentFullName { get; set; }

    [Display(Name = "Trainers")]
    public string LatestClassroomsTrainers { get; set; }

    [Display(Name = "Latest_Classroom")]
    public string LatestClassroom { get; set; }

    [Display(Name = "Student_Classrooms")]
    public List<string> StudentClassroomNames { get; set; }
    [Display(Name = "Exam_Type")]
    public ExamType ExamType { get; set; }

    [Display(Name = "Excuse")]
    public string? ExcuseDescription { get; set; }

    [Display(Name = "StudentExamsCount")]
    public int? StudentExamsCount { get; set; }

    [Display(Name = "Status")]
    public Status Status { get; set; }
}
