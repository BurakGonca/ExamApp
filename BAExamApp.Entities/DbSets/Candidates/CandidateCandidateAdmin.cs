namespace BAExamApp.Entities.DbSets.Candidates;

public class CandidateCandidateAdmin : BaseUser
{
    // Area name + Entity name = Candidate + CandidateAdmin

    // NAV PROP
    public Guid CandidateBranchId { get; set; }
    public virtual CandidateCandidateBranch CandidateBranch { get; set; }

}
