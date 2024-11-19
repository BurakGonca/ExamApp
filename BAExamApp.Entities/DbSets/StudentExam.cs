namespace BAExamApp.Entities.DbSets;

public class StudentExam : AuditableEntity
{
    public StudentExam()
    {
        StudentQuestions = new HashSet<StudentQuestion>();
    }

    public decimal? Score { get; set; }
    public bool IsFinished { get; set; } = false;
    public bool? IsReadRules { get; set; }

    public int AnsweredQuestionCount { get; set; } = 0;
    public string? ExcuseDescription { get; set; }
    public bool RetakeExam { get; set; } = false;
    public DateTime? RetakeExamDate { get; set; }
    public bool IsEmailSent { get; set; } = false;

    //Navigation Prop.
    public Guid ExamId { get; set; }
    public virtual Exam? Exam { get; set; }
    public Guid StudentId { get; set; }
    public virtual Student? Student { get; set; }
    public Guid? EvaluatorId { get; set; }
    public virtual Trainer? Evaluator { get; set; }

    public virtual ICollection<StudentQuestion> StudentQuestions { get; set; }
}