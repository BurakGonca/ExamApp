namespace BAExamApp.MVC.Areas.Admin.Models.DashboardVMs;

public class DashboardEventVM
{
    public Guid Id { get; set; }
    public string Title { get; set; } 
    public DateTime Start { get; set; } 
    public DateTime End { get; set; } 
    public string Description { get; set; }
}