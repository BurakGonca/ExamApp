namespace BAExamApp.Entities.DbSets.Candidates;
public class CandidateAnswer : AuditableEntity
{
    // Area name + Entity name = Candidate + Answer
    // genel soruların cevabını tutmak için

    public string Answer { get; set; }
    public bool IsImageAnswer { get; set; }
    public bool IsRightAnswer { get; set; }

    // NAV PROP
    public Guid QuestionId { get; set; }
    public virtual CandidateQuestion Question { get; set; }
}
