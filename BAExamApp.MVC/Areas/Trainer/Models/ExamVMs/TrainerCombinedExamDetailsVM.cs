namespace BAExamApp.MVC.Areas.Trainer.Models.ExamVMs;

public class TrainerCombinedExamDetailsVM
{
    public TrainerExamDetailVM ExamDetail { get; set; }

    public IEnumerable<StudentExamDetailForTrainerVM> StudentExamDetails { get; set;}
}
