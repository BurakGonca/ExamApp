using BAExamApp.Core.Enums;
using BAExamApp.Entities.DbSets;
using BAExamApp.Entities.Enums;
using BAExamApp.MVC.Areas.Admin.Models.ExamRuleSubtopicVMs;
using System.ComponentModel.DataAnnotations;

namespace BAExamApp.MVC.Areas.Admin.Models.ExamRuleVMs;

public class AdminExamRuleDetailsVM
{
    public Guid Id { get; set; }
    [Display(Name = "Exam_Rule_Name")]
    public string Name { get; set; }

    [Display(Name = "Exam_Rule_Description")]
    public string? Description { get; set; }

    [Display(Name = "Product")]
    public string ProductName { get; set; }

    public bool IsRuleUsed { get; set; }

    [Display(Name = "Status")]
    public Status Status { get; set; }

    public List<AdminExamRuleSubtopicDetailVM> ExamRuleSubtopics { get; set; }
}