using BAExamApp.MVC.Areas.Admin.Models.ExamVMs;
using BAExamApp.MVC.Areas.Admin.Models.QuestionVMs;

namespace BAExamApp.MVC.Areas.Admin.Models.DashboardVMs;

public class DashboardVM
{
    public List<AdminQuestionListVM>? AwaitedQuestion { get; set; }
    public List<AdminQuestionListVM>? ApprovedQuestions { get; set; }
    public List<AdminQuestionListVM>? RevisionQuestions { get; set; }
    public List<StudentExamsForAdminVM> Students { get; set; }
    public DashboardOverviewVM Overview { get; set; }
    public List<StudentExamsForAdminVM> TopRatedStudents { get; set; }
    public List<DashboardEventVM> Events { get; set; }
    public List<ActiveStudentsExamsForAdminVM> ActiveTopRatedStudents { get; set; }   
}
