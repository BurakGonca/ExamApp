using BAExamApp.Core.Enums;
using System.ComponentModel.DataAnnotations;

namespace BAExamApp.MVC.Areas.CandidateAdmin.Models.CandidateBranchVMs;

public class CandidateBranchListVM
{
    public Guid Id { get; set; }

    [Display(Name = "Branch_Name")]
    public string Name { get; set; }
    public Status Status { get; set; }
}
