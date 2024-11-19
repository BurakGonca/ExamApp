using System.ComponentModel.DataAnnotations;

namespace BAExamApp.MVC.Areas.Admin.Models.BranchVMs;

public class AdminBranchDetailsVM
{
    public Guid Id { get; set; }

    [Display(Name = "Branch_Name")]
    public string Name { get; set; }

    public bool IsBranchUsedInClass { get; set; }
}