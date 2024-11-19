namespace BAExamApp.Entities.DbSets.Candidates;

public class CandidateCandidateAnswer : BaseEntity
{
    // Area name + Entity name = Candidate + CandidateAnswer
    // Adayların verdiği cevapları tutmak için

    public string? Answer { get; set; }
    public bool? IsRightAnswer { get; set; }
    public string? AIAssessment { get; set; }

    // NAV PROP
    public Guid CandidateQuestionId { get; set; }
    public Guid? CandidateAnswerId { get; set; }
    public virtual CandidateCandidateQuestion CandidateQuestion { get; set; }
}
