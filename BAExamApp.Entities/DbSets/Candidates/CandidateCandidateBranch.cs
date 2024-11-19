namespace BAExamApp.Entities.DbSets.Candidates;

public class CandidateCandidateBranch : AuditableEntity
{
    // Area name + Entity name = Candidate + CandidateBranch
    // adayların şubelerini tutmak için

    public CandidateCandidateBranch()
    {
        CandidateAdmins = new HashSet<CandidateCandidateAdmin>();
        CandidateGroups = new HashSet<CandidateCandidateGroup>();
    }
    public string Name { get; set; }

    // NAV PROP
    public virtual ICollection<CandidateCandidateAdmin> CandidateAdmins { get; set; }
    public virtual ICollection<CandidateCandidateGroup> CandidateGroups { get; set; }
}
