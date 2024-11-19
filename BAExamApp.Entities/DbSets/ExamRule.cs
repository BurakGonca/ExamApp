namespace BAExamApp.Entities.DbSets;

public class ExamRule : AuditableEntity
{
    public ExamRule()
    {
        Exams = new HashSet<Exam>();
        ExamRuleSubtopics = new HashSet<ExamRuleSubtopic>();
    }
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    

    //Navigation Prop.
    public Guid ProductId { get; set; }
    public virtual Product? Product { get; set; }

    public virtual ICollection<Exam> Exams { get; set; }
    public virtual ICollection<ExamRuleSubtopic> ExamRuleSubtopics { get; set; }

}