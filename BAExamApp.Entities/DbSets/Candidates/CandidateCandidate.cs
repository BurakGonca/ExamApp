namespace BAExamApp.Entities.DbSets.Candidates;

// Area name + Entity name = Candidate + Candidate
public class CandidateCandidate : AuditableEntity
{
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public byte[]? Image { get; set; }

    public bool IsRetakeAllowed { get; set; } = true;

    public CandidateCandidate()
    {
        Groups = new HashSet<CandidatesGroups>();
        Exams = new HashSet<CandidatesExams>();
    }

    // NAV PROP
    public virtual ICollection<CandidatesGroups> Groups { get; set; }
    public virtual ICollection<CandidatesExams> Exams { get; set; }


}
