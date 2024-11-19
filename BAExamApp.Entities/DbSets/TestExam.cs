namespace BAExamApp.Entities.DbSets;

public class TestExam : AuditableEntity
{
    public TestExam()
    {
        TestExamQuestions = new HashSet<TestExamQuestion>();
        TestExamTesters = new HashSet<TestExamTester>();
    }

    public string Name { get; set; } = null!;
    public DateTime TestExamDate { get; set; }
    public DateTime StartTime { get; set; }
    public TimeSpan TestExamDuration { get; set; }
    public State State { get; set; }
    public string? Description { get; set; }

    //Navigation Prop.
    public virtual ICollection<TestExamQuestion> TestExamQuestions { get; set; }
    public virtual ICollection<TestExamTester> TestExamTesters { get; set; }
}
