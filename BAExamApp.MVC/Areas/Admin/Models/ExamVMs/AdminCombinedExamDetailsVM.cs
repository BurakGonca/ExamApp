namespace BAExamApp.MVC.Areas.Admin.Models.ExamVMs;

public class AdminCombinedExamDetailsVM
{
    public AdminExamDetailVM ExamDetail { get; set; }
    public IEnumerable<StudentExamDetailForAdminVM> StudentExamDetails { get; set; }
}