using BAExamApp.Dtos.Candidate.CandidateGroups;
using BAExamApp.Dtos.Candidate.Candidates;
using System.ComponentModel.DataAnnotations;

namespace BAExamApp.MVC.Areas.CandidateAdmin.Models.CandidateCandidateVMs;

public class CandidateCandidateDetailsVM
{
    public Guid Id { get; set; }

    [Display(Name = "First_Name")]
    public string FirstName { get; set; } = null!;

    [Display(Name = "Last_Name")]
    public string LastName { get; set; } = null!;

    [Display(Name = "Email")]
    public string Email { get; set; } = null!;

    [Display(Name = "Profile_Image")]
    public byte[]? Image { get; set; } = null!;

    [Display(Name = "Group_Name")]
    public List<string> GroupNames { get; set; } = null!; // Boş liste başlatıldı
    public List<CandidateGroupsDetailsDto> CandidateGroups { get; set; } = null!; // Boş liste başlatıldı
    public List<CandidateExamResultDto> Exams { get; set; }
}
