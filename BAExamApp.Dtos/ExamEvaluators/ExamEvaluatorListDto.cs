namespace BAExamApp.Dtos.ExamEvaluators;

public class ExamEvaluatorListDto
{
    public Guid ExamId { get; set; }
    public string ExamName { get; set; }
    public DateTime ExamDateTime { get; set; }
    public TimeSpan ExamDuration { get; set; }
    public List<string> ClassroomNames { get; set; }
}
