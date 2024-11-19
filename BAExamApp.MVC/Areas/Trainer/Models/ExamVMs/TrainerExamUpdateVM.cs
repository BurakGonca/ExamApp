using BAExamApp.Entities.Enums;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;
using NuGet.Protocol.Plugins;
using System.ComponentModel.DataAnnotations;

namespace BAExamApp.MVC.Areas.Trainer.Models.ExamVMs;

public class TrainerExamUpdateVM
{

    public Guid Id { get; set; }

    [Display(Name = "Exam_Name")]
    public string Name { get; set; } = null!;

    [Display(Name = "Exam_Date_and_Time")]
    public DateTime ExamDateTime { get; set; } = DateTime.Now;


    [Display(Name = "Exam_Duration")]
    public TimeSpan ExamDuration { get; set; } = TimeSpan.FromHours(03.00);


    [Display(Name = "Exam_Type")]
    public ExamType ExamType { get; set; }


    [Display(Name = "Exam_Rule")]
    public Guid ExamRuleId { get; set; }


    [Display(Name = "Exam_Creation_Type")]
    public ExamCreationType ExamCreationType { get; set; }


    [Display(Name = "Classroom")]
    public List<Guid> ExamClassroomsIds { get; set; }


    [Display(Name = "Max_Score")]
    public decimal MaxScore { get; set; } = 100;


    [Display(Name = "Bonus_Score")]
    public decimal BonusScore { get; set; }

    public bool forClassroom { get; set; } = true;


    [Display(Name = "Student")]
    public List<Guid> StudentIds { get; set; }


    [Display(Name = "Description")]
    public string? Description { get; set; }


    [Display(Name = "Trainer Explanation")]
    public string? TrainerExplanation { get; set; }
}
