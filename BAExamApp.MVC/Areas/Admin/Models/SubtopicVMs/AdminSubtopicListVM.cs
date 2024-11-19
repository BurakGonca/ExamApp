using BAExamApp.Core.Enums;
using System.ComponentModel.DataAnnotations;

namespace BAExamApp.MVC.Areas.Admin.Models.SubtopicVMs;
public class AdminSubtopicListVM
{
    public Guid Id { get; set; }

    [Display(Name ="Subtopic")]
    public string Name { get; set; }

    [Display(Name = "Subject")]
    public string SubjectName { get; set; }

    public bool IsActive { get; set; }

    [Display(Name = "Status")]
    public Status Status { get; set; }
}
