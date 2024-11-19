using BAExamApp.Core.Enums;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BAExamApp.MVC.Areas.CandidateAdmin.Models.CandidateGroupVMs;

public class CandidateGroupEditVM
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public Guid CandidateBranchId { get; set; }
    public SelectList? BranchList { get; set; }
    public Status? Status { get; set; }
}
