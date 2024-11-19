namespace BAExamApp.Dtos.StudentExams;

public class StudentExamUpdateDto
{
    public Guid Id { get; set; }
    public decimal? Score { get; set; } = null;
    public bool IsFinished { get; set; } = false;
    public bool IsReadRules { get; set; }
    public int AnsweredQuestionCount { get; set; }
    public Guid ExamId { get; set; }
    public Guid StudentId { get; set; }
    public Guid? EvaluatorId { get; set; }
    public DateTime RetakeExamDate { get; set; }

    public string? ExcuseDescription { get; set; }
}
