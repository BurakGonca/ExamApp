using BAExamApp.Core.Enums;
using BAExamApp.Dtos.Candidate.Candidates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAExamApp.Dtos.Candidate.CandidateGroups;
public class CandidateGroupDetailsDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string BranchName { get; set; }
    public Guid CandidateBranchId { get; set; }
    public Status Status { get; set; }
    public List<CandidateDetailsDto> Candidates { get; set; }
}
