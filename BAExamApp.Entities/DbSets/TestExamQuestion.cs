namespace BAExamApp.Entities.DbSets;

public   class TestExamQuestion : AuditableEntity
{
    public Guid TestExamId { get; set; }
    public virtual TestExam? TestExam { get; set; }
    public Guid QuestionId { get; set; }
    public virtual Question? Question { get; set; }
}
