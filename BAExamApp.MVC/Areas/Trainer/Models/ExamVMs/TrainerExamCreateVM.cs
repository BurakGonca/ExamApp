using BAExamApp.Dtos.ExamClassrooms;
using BAExamApp.Dtos.ExamRules;
using BAExamApp.Entities.Enums;
using BAExamApp.MVC.ModelBinders;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace BAExamApp.MVC.Areas.Trainer.Models.ExamVMs;

public class TrainerExamCreateVM
{
    [Display(Name = "Exam_Date_and_Time")]
    [ModelBinder(BinderType = typeof(CustomDateTimeModelBinder))]
    public DateTime ExamDateTime { get; set; } 
    [Display(Name = "Exam_Duration")]
    public TimeSpan ExamDuration { get; set; } = TimeSpan.FromHours(03.00);
    [Display(Name = "Description")]
    public string? Description { get; set; }

    [Display(Name = "Exam_Type")]
    public ExamType ExamType { get; set; }

    [Display(Name = "Exam_Creation_Type")]
    public ExamCreationType ExamCreationType { get; set; }

    [Display(Name = "Max_Score")]
    public decimal MaxScore { get; set; } = 100;

    [Display(Name = "Bonus_Score")]
    public decimal BonusScore { get; set; }

    public bool IsCanceled { get; set; } = false;
    public bool IsTest { get; set; } = false;

    public bool forClassroom { get; set; } = true;

    [Display(Name = "Exam_Rule")]
    public Guid ExamRuleId { get; set; }
    [Display(Name = "Classroom")]
    public List<Guid> ExamClassroomsIds { get; set; }
    [Display(Name = "Student")]
    public List<Guid> StudentIds { get; set; }
    public IEnumerable<ExamRuleDto> ExamRules { get; set; }
}