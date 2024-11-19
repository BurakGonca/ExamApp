namespace BAExamApp.Entities.DbSets.Candidates;

public class CandidateCandidateGroup : AuditableEntity
{
    // Area name + Entity name = Candidate + CandidateGroup
    // Adayların gruplarını tutmak için

    public CandidateCandidateGroup()
    {
        Candidates = new HashSet<CandidatesGroups>();
    }

    public string Name { get; set; }

    //  NAV PROP
    public Guid CandidateBranchId { get; set; }
    public virtual CandidateCandidateBranch CandidateBranch { get; set; }
    public virtual ICollection<CandidatesGroups> Candidates { get; set; } // ara tablo bağlantısı
}
