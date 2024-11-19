namespace BAExamApp.Entities.DbSets.Candidates;

public class CandidateQuestion : AuditableEntity
{
    // Area name + Entity name = Candidate + Qustion
    // genel soruları tutmak için

    public CandidateQuestion()
    {
        QuestionAnswers = new HashSet<CandidateAnswer>();
        CandidateCandidateQuestion = new HashSet<CandidateCandidateQuestion>();
    }

    public string? Content { get; set; }
    public CandidateQuestionType CandidateQuestionType { get; set; }
    public byte[]? Image { get; set; } 
    public bool IsActive { get; set; } = true;

    // NAV PROP
    public virtual ICollection<CandidateCandidateQuestion> CandidateCandidateQuestion { get; set; }
    public virtual ICollection<CandidateAnswer> QuestionAnswers { get; set; }
    public Guid CandidateQuestionSubjectId { get; set; }
    public virtual CandidateQuestionSubject CandidateQuestionSubject { get; set; }

}
