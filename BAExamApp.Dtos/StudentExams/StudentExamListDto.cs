namespace BAExamApp.Dtos.StudentExams;

public class StudentExamListDto
{
    public Guid Id { get; set; }
    public decimal? Score { get; set; }
    public bool IsFinished { get; set; }
    public string ExamName { get; set; }
    public string MaxScore { get; set; }
    public DateTime ExamDateTime { get; set; }
    public TimeSpan ExamDuration { get; set; }
    public Guid StudentId { get; set; }
    public string StudentName { get; set; }
    public string? EvaluatorName { get; set; }
    public string? ExcuseDescription { get; set; }
    public bool RetakeExam { get; set; }
    public List<string> ClassroomNames { get; set; }
}
