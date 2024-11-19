namespace BAExamApp.Dtos.StudentExams;

public class StudentExamListForTrainerDto
{
    public Guid Id { get; set; }
    public decimal? Score { get; set; }
    public string ExamName { get; set; }
    public DateTime ExamDateTime { get; set; }
    public decimal? MaxScore { get; set; }
}
