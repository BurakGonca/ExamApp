namespace BAExamApp.Dtos.Candidate.Candidates;
public class CandidateListDto
{
    public Guid Id { get; set; }
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public byte[]? Image { get; set; }

    public bool? IsRetakeAllowed { get; set; }
}
