namespace BAExamApp.MVC.Areas.Trainer.Models.StudentExamVMs;

public class TrainerStudentExamListVM
{
    public Guid Id { get; set; }
    public decimal? Score { get; set; }
    public string ExamName { get; set; }
    public DateTime ExamDateTime { get; set; }
    public decimal? MaxScore { get; set; }
}
