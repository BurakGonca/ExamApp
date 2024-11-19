using BAExamApp.Dtos.ExamRules;
using BAExamApp.Entities.DbSets;
using BAExamApp.Entities.Enums;
using BAExamApp.MVC.ModelBinders;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace BAExamApp.MVC.Areas.Admin.Models.ExamVMs;

public class AdminExamCreateVM
{
    [Display(Name = "Exam_Date_and_Time")]
    [ModelBinder(BinderType = typeof(CustomDateTimeModelBinder))]
    public DateTime ExamDateTime { get; set; } = DateTime.Now;

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

    public bool forClassroom { get; set; } = true;
    public bool IsTest { get; set; } = false;

    public bool IsCanceled { get; set; } = false;

    [Display(Name = "Exam_Rule")]
    public Guid ExamRuleId { get; set; }

    [Display(Name = "Classroom")]
    public List<Guid> ExamClassroomsIds { get; set; }

    [Display(Name = "Student")]
    public List<Guid> StudentIds { get; set; }
    public IEnumerable<ExamRuleDto> ExamRules { get; set; }
}
