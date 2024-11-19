namespace BAExamApp.Entities.DbSets;

public class QuestionDifficulty : AuditableEntity
{
    public QuestionDifficulty()
    {
        ExamRuleSubtopics = new HashSet<ExamRuleSubtopic>();
        Questions = new HashSet<Question>();
    }

    public string Name { get; set; }
    public TimeSpan TimeGiven { get; set; }
    public double ScoreCoefficient { get; set; }

    //Navigation Prop.
    public virtual ICollection<ExamRuleSubtopic> ExamRuleSubtopics { get; set; }
    public virtual ICollection<Question> Questions { get; set; }
}
