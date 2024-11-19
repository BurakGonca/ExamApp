using BAExamApp.MVC.Areas.Trainer.Models.ExamVMs;
using BAExamApp.MVC.Areas.Trainer.Models.StudentExamVMs;
using BAExamApp.MVC.Areas.Trainer.Models.DashboardVMs;

namespace BAExamApp.MVC.Areas.Trainer.Models.DashboardVMs;

public class TrainerDashboardVM
{
    public List<DashboardEventVM> Events { get; set; }
    public List<StudentExamsForTrainerVM> TrainerTopratedStudents { get; set; }

    public List<AllStudentExamForTrainerVM> AllTopratedStudents { get; set; }

    public List<ActiveStudentExamForTrinerVM> ActiveStudents { get; set; }

    public List<DashboardQuestionVM> QuestionList { get; set; }
}
