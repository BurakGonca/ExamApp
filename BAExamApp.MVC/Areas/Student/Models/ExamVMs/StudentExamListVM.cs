namespace BAExamApp.MVC.Areas.Student.Models.ExamVMs;

public class StudentExamListVM
{
    public Guid Id { get; set; }
    public string ExamName { get; set; }
    public DateTime ExamDateTime { get; set; }
    public TimeSpan ExamDuration { get; set; }
    public decimal? Score { get; set; }
}
