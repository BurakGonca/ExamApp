using BAExamApp.Dtos.ExamEvaluators;
using BAExamApp.Entities.Enums;
using System.ComponentModel.DataAnnotations;

namespace BAExamApp.MVC.Areas.Admin.Models.ExamVMs;

public class AdminExamDetailVM
{
    public Guid Id { get; set; }

    [Display(Name = "Exam_Name")]
    public string Name { get; set; }

    [Display(Name = "Exam_Date")]
    public DateTime ExamDateTime { get; set; }

    [Display(Name = "Exam_Duration")]
    public TimeSpan ExamDuration { get; set; }

    [Display(Name = "Max_Score")]
    public decimal MaxScore { get; set; }

    [Display(Name = "Bonus_Score")]
    public decimal BonusScore { get; set; }

    [Display(Name = "Exam_Rule")]
    public string ExamRuleName { get; set; }

    [Display(Name = "Classroom")]
    public string ClassroomName { get; set; }

    [Display(Name = "Exams_Evaluators")]
    public List<ExamEvaluatorListForExamDetailsDto> ExamsEvaluators { get; set; }


    [Display(Name = "Exam_Type")]
    public ExamType ExamType { get; set; }

    [Display(Name = "Excuse")]
    public string? ExcuseDescription { get; set; }

}
