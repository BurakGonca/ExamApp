namespace BAExamApp.Entities.DbSets;

public class StudentQuestion : AuditableEntity
{
    public StudentQuestion()
    {
        StudentAnswers = new HashSet<StudentAnswer>();
    }

    public int MaxScore { get; set; }
    public int BonusScore { get; set; } = 0;
    public int? Score { get; set; }
    public int QuestionOrder { get; set; }
    public DateTime? TimeStarted { get; set; }
    public DateTime? TimeFinished { get; set; }

    //Navigation Prop.
    public Guid StudentExamId { get; set; }
    public virtual StudentExam? StudentExam { get; set; }
    public Guid QuestionId { get; set; }
    public virtual Question? Question { get; set; }

    public virtual ICollection<StudentAnswer> StudentAnswers { get; set; }
}
