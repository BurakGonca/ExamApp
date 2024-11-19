using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using BAExamApp.Entities.Enums;

namespace BAExamApp.MVC.Areas.Admin.Models.ExamVMs;

public class AdminExamListVM
{
    public Guid Id { get; set; }

    [DisplayName("Name_Of_The_Exam")]
    public string Name { get; set; }

    [DisplayName("Exam_Type")]
    public ExamType ExamType { get; set; }

    [DisplayName("Class_Name")]
    public List<string>? ClassroomNames { get; set; }

    [Display(Name = "Exam_Date")]
    public DateTime ExamDateTime { get; set; } = DateTime.Now;

    [Display(Name = "Exam_Duration")]
    public TimeSpan ExamDuration { get; set; }

    [DisplayName("Question_CreatedDate")]
    public DateTime CreatedDate { get; set; }

    [DisplayName("Exam_Rule_Name")]
    public string ExamRuleName { get; set; }

    public bool IsStarted { get; set; }
    public bool? IsTest { get; set; }

    [DisplayName("Cancellation_Status")]
    public bool? IsCanceled { get; set; }

    [DisplayName("Student_Name")]
    public List<string> StudentName { get; set; }

    [DisplayName("Student_Exam_Score")]
    public List<decimal?> StudentExamScore { get; set; }
    public List<(string?, decimal?, string?)> tooltipStudentList { get; set; }

    [Display(Name = "Class_Grade_Average")]
    public decimal? ClassGradeAverage { get; set; }
    [Display(Name = "Student_Count")]
    public int? StudentCount { get; set; }

    [Display(Name = "Student_Exam_Score_Count")]
    public int? StudentExamScoreCount { get; set; }
}
