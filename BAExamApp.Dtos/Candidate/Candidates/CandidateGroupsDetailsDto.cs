using BAExamApp.Core.Enums;
using BAExamApp.Dtos.Candidate.CandidateGroups;
using BAExamApp.Dtos.Candidate.CandidatesGroups;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAExamApp.Dtos.Candidate.Candidates;
public class CandidateGroupsDetailsDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string BranchName { get; set; }
    public Guid GroupId { get; set; } 
    public string GroupName { get; set; }
    public Status Status { get; set; }
    public List<CandidateDetailsDto> Candidates { get; set; }
}
