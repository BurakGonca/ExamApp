using BAExamApp.Core.Enums;
using BAExamApp.Dtos.Candidate.CandidateGroups;

namespace BAExamApp.Dtos.Candidate.Candidates;
public class CandidateDetailsDto
{
    public Guid Id { get; set; }
    public Guid CandidateId { get; set; }
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public byte[]? Image { get; set; }
}
