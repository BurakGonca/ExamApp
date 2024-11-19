namespace BAExamApp.Entities.DbSets.Candidates;

public class CandidateCandidateQuestion : AuditableEntity
{
    public int MaxScore { get; set; }
    public int? Score { get; set; }
    public int QuestionOrder { get; set; }
    public DateTime? TimeStarted { get; set; }
    public DateTime? TimeFinished { get; set; }

    // NAV PROP
    public Guid QuestionId { get; set; }
    public Guid CandidatesExamsId { get; set; }
    public Guid? CandidateAnswerId { get; set; }
    public virtual CandidateQuestion Question { get; set; }
    public virtual CandidatesExams CandidatesExams { get; set; }
    public virtual CandidateCandidateAnswer? CandidateAnswer { get; set; }
}
