namespace BAExamApp.Entities.DbSets;

public class Subtopic : AuditableEntity
{
    public Subtopic()
    {
        ExamRuleSubtopics = new HashSet<ExamRuleSubtopic>();
        QuestionSubtopics = new HashSet<QuestionSubtopics>();
    }
    public string Name { get; set; }

    //Navigation Prop.
    public Guid SubjectId { get; set; }
    public virtual Subject? Subject { get; set; }

    public virtual ICollection<ExamRuleSubtopic> ExamRuleSubtopics { get; set; }
    public virtual ICollection<QuestionSubtopics> QuestionSubtopics { get; set; }
}
