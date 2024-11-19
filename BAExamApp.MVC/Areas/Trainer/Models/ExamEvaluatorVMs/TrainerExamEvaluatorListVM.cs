namespace BAExamApp.MVC.Areas.Trainer.Models.ExamEvaluatorVMs;

public class TrainerExamEvaluatorListVM
{
    public Guid ExamId { get; set; }
    public string ExamName { get; set; }
    public DateTime ExamDateTime { get; set; }
    public TimeSpan ExamDuration { get; set; }
    public List<string> ClassroomNames { get; set; }
}
