namespace BAExamApp.MVC.Areas.CandidateAdmin.Models.CandidateExamVMs;

public class CandidateExamListVM
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public DateTime ExamDateTime { get; set; }
    public TimeSpan ExamDuration { get; set; }
    public bool IsStarted { get; set; } = false;
    public DateTime? ExamLinkEndDate { get; set; }

}
