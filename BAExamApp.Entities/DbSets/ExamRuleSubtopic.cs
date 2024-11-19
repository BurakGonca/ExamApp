namespace BAExamApp.Entities.DbSets;

public class ExamRuleSubtopic : BaseEntity
{
    public ExamType ExamType { get; set; }
    public QuestionType QuestionType { get; set; }
    public int QuestionCount { get; set; }

    //Navigation Prop.
    public Guid QuestionDifficultyId { get; set; }
    public virtual QuestionDifficulty? QuestionDifficulty { get; set; }
    public Guid ExamRuleId { get; set; }
    public virtual ExamRule? ExamRule { get; set; }
    public Guid SubtopicId { get; set; }
    public virtual Subtopic? Subtopic { get; set; }
}
