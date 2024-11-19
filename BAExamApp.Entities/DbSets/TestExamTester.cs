namespace BAExamApp.Entities.DbSets;

public class TestExamTester : BaseEntity
{
    public Guid TestExamId { get; set; }
    public virtual TestExam? TestExam { get; set; }
    public Guid TesterId { get; set; }
    public virtual Trainer? Tester { get; set; }
}
