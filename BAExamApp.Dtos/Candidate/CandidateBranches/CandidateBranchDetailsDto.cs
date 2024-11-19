using BAExamApp.Dtos.Candidate.CandidateGroups;

namespace BAExamApp.Dtos.Candidate.CandidateBranches;

public class CandidateBranchDetailsDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public List<CandidateGroupDetailsDto> CandidateGroups { get; set; }
}
