using BAExamApp.Core.Enums;
using BAExamApp.Dtos.Candidate.CandidateGroups;
using BAExamApp.Dtos.Candidate.Candidates;
using System.ComponentModel.DataAnnotations;

namespace BAExamApp.MVC.Areas.CandidateAdmin.Models.CandidateGroupVMs;

public class CandidateGroupDetailVM
{
    public Guid Id { get; set; }

    [Display(Name = "Name")]
    public string Name { get; set; }
    public Guid CandidateBranchId { get; set; }

    [Display(Name = "Branch_Name")]
    public string BranchName { get; set; }
    public Status? Status { get; set; }

    [Display(Name = "Candidate_Name")]
    public List<string> CandidateNames { get; set; } = new List<string>(); // Boş liste başlatıldı
    public List<CandidateDetailsDto> Candidates { get; set; }
}
