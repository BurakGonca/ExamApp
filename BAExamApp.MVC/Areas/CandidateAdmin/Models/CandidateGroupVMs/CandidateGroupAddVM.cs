using BAExamApp.Dtos.Candidate.CandidateBranches;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;

namespace BAExamApp.MVC.Areas.CandidateAdmin.Models.CandidateGroupVMs
{
    public class CandidateGroupAddVM
    {
        public string Name { get; set; }
        public Guid CandidateBranchId { get; set; }
        public SelectList? BranchList { get; set; }
    }
}
