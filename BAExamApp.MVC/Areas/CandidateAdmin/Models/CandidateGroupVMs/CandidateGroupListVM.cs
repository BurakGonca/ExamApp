using BAExamApp.Core.Enums;
using BAExamApp.Entities.DbSets;
using BAExamApp.Entities.DbSets.Candidates;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BAExamApp.MVC.Areas.CandidateAdmin.Models.CandidateGroupVMs;

public class CandidateGroupListVM
{
    public Guid Id { get; set; }
    
    [Display(Name = "Name")]
    public string Name { get; set; }
    public Guid CandidateBranchId { get; set; }
   
    [Display(Name = "Branch_Name")]
    public string BranchName { get; set; }
    [DisplayName("Status")]
    public Status Status { get; set; }
    public int? CandidateStatus { get; set; }
}
