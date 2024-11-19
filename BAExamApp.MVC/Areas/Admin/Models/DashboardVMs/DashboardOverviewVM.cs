namespace BAExamApp.MVC.Areas.Admin.Models.DashboardVMs;

public class DashboardOverviewVM
{
    public int StudentCount { get; set; }
    public int TrainerCount { get; set; }
    public int ExamCount { get; set; }
    public decimal SuccessRate { get; set; }    
}
