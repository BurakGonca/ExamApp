using BAExamApp.MVC.Areas.Trainer.Models.StudentExamVMs;

namespace BAExamApp.MVC.Areas.Trainer.Models.StudentVMs;

public class TrainerStudentDetailsVM
{
    public Guid Id { get; set; }
    public string StudentName { get; set; }
    public List<TrainerStudentExamListVM> StudentExams{ get; set; }
}
