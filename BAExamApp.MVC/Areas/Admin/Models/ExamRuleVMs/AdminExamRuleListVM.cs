using BAExamApp.Core.Enums;
using BAExamApp.Entities.Enums;
using System.ComponentModel.DataAnnotations;

namespace BAExamApp.MVC.Areas.Admin.Models.ExamRuleVMs;

public class AdminExamRuleListVM
{
    public Guid Id { get; set; }

    [Display(Name = "Exam_Rule_Name")]
    public string Name { get; set; }

    [Display(Name = "Created_Date")]
    public DateTime CreatedDate { get; set; }

    [Display(Name = "Created_By")]
    public string CreatedByPerson { get; set; }

    [Display(Name = "Status")]
    public Status Status { get; set; }
}