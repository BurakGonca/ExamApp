using BAExamApp.Core.Enums;
using System.ComponentModel.DataAnnotations;

namespace BAExamApp.MVC.Areas.CandidateAdmin.Models.CandidateQuestionSubjectVMs;

public class CandidateAdminQuestionSubjectDetailVM
{
    public Guid Id { get; set; }
    [Display(Name = "Subject")]
    public string Name { get; set; }

    public bool IsQuestionUsed { get; set; }
    public Status Status { get; set; }
}
