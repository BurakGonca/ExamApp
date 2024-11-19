using BAExamApp.Core.Enums;
using BAExamApp.Dtos.Candidate.CandidateGroups;
using BAExamApp.Dtos.TrainerClassrooms;
using System.ComponentModel.DataAnnotations;

namespace BAExamApp.MVC.Areas.CandidateAdmin.Models.CandidateBranchVMs;

public class CandidateBranchDetailsVM
{
    public Guid Id { get; set; }

    [Display(Name = "Branch_Name")]
    public string Name { get; set; }
    public Status Status { get; set; }

    public bool ShowPassiveGroups { get; set; } // New property
    [Display(Name = "Group_Name")]
    public List<string> GroupNames { get; set; } = new List<string>(); // Boş liste başlatıldı
    public List<CandidateGroupDetailsDto> CandidateGroups { get; set; }
}
