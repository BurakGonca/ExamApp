namespace BAExamApp.Entities.DbSets;

public class QuestionAnswer : BaseEntity
{
    public QuestionAnswer()
    {
        StudentAnswers = new HashSet<StudentAnswer>();
    }

    public string Answer { get; set; } = null!;
    public bool IsRightAnswer { get; set; }
    public bool IsAnswerImage { get; set; }

    //Navigation Prop.
    public Guid QuestionId { get; set; }
    public virtual Question? Question { get; set; }

    public virtual ICollection<StudentAnswer> StudentAnswers { get; set; }
}