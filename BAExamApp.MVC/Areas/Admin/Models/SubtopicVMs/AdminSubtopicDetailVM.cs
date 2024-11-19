using BAExamApp.Core.Enums;
using BAExamApp.Dtos.Subjects;
using System.ComponentModel.DataAnnotations;

namespace BAExamApp.MVC.Areas.Admin.Models.SubtopicVMs;

public class AdminSubtopicDetailVM
{
    public Guid Id { get; set; }

    [Display(Name = "Subtopic")]
    public string Name { get; set; }

    [Display(Name = "IsActive")]
    public bool? IsActive { get; set; }
    [Display(Name = "Subject")]
    public string? SubjectName { get; set; }
    public bool IsQuestionUsed { get; set; }
    public bool IsExamRuleUsed { get; set; }
    
    public Status Status { get; set; }
}
