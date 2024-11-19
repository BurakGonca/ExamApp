namespace BAExamApp.Entities.DbSets;

public class Question : AuditableEntity
{
    public Question()
    {
        StudentQuestions = new HashSet<StudentQuestion>();
        QuestionAnswers = new HashSet<QuestionAnswer>();
        QuestionRevisions = new HashSet<QuestionRevision>();
        TestExamsQuestions = new HashSet<TestExamQuestion>();
        QuestionFeedbacks = new HashSet<QuestionFeedback>();
        QuestionSubtopics = new HashSet<QuestionSubtopics>();
    }

    public string Content { get; set; } = null!;
    public State State { get; set; } = State.Awaited;
    public QuestionType QuestionType { get; set; }
    public string? Image { get; set; }
    public bool IsActive { get; set; } = true;
    public string Target { get; set; }
    public string Gains { get; set; }
    public string? RejectComment { get; set; }

    //Navigation Prop.
    public Guid? SubjectId { get; set; }
    public virtual Subject? Subject { get; set; }
    public Guid QuestionDifficultyId { get; set; }
    public virtual QuestionDifficulty? QuestionDifficulty { get; set; }

    public virtual ICollection<StudentQuestion> StudentQuestions { get; set; }
    public virtual ICollection<QuestionAnswer> QuestionAnswers { get; set; }
    public virtual ICollection<QuestionRevision> QuestionRevisions { get; set; }
    public virtual ICollection<QuestionFeedback> QuestionFeedbacks { get; set; }
    public virtual ICollection<TestExamQuestion> TestExamsQuestions { get; set; }
    public virtual ICollection<QuestionSubtopics> QuestionSubtopics { get; set; }

}
