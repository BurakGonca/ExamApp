using BAExamApp.Dtos.StudentQuestions;

namespace BAExamApp.Dtos.StudentExams;
public class StudentExamDto
{
    public Guid Id { get; set; }
    public decimal? Score { get; set; }
    public bool IsFinished { get; set; }
    public int AnsweredQuestionCount { get; set; }
    public bool IsReadRules { get; set; }
    public Guid ExamId { get; set; }
    public Guid StudentId { get; set; }
    public string StudentFullName { get; set; }
    public Guid? EvaluatorId { get; set; }
    public List<StudentQuestionDto> StudentQuestions { get; set; }
    public string? ExcuseDescription { get; set; }
    public bool RetakeExam { get; set; }
    public DateTime RetakeExamDate { get; set; }

}
